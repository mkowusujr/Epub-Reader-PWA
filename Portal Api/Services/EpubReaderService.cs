namespace Portal_Api.Services;
using EpubSharp;
using AngleSharp;
using AngleSharp.Html.Dom;

/// <summary>
/// Service wrapper to parse epub files
/// </summary>
public static class EpubReaderService
{
    /// <summary>
    /// The parsed epub file
    /// </summary>
    private static EpubBook Ebook { get; set; }

    /// <summary>
    /// Parses an epub file and stores it in a EpubBook object
    /// </summary>
    /// <param name="filePath"></param>
    public static void OpenEBook(string filePath) {
        Ebook = EpubReader.Read(filePath);
    }

    /// <summary>
    /// Gets the parsed epub's title
    /// </summary>
    /// <returns>The parsed epub's title</returns>
    public static string GetTitle() {
        return Ebook.Title;
    }

    /// <summary>
    /// Gets the parsed epub's author(s)
    /// </summary>
    /// <returns>The parsed epub's author(s)</returns>
    public static string GetAuthors() {
        List<string> authors = Ebook.Authors.ToList();
        return authors.Count == 1 ? authors[0] : String.Join(", ", authors);
    }

    /// <summary>
    /// Gets the table of contents for the epub and removes
    /// unneeded info from the anchor elements
    /// </summary>
    /// <returns>A string containing multiple anchor elements</returns>
    public static string GetTableOfContents() {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        using var parsedDom = context.OpenAsync(req => req.Content(Ebook.Resources.Html.ElementAt(0).TextContent)).Result;

        var anchorElements = parsedDom.QuerySelectorAll("a")
                            .OfType<IHtmlAnchorElement>()
                            .UpdateDomElements(a => a.Href = a.Href.Split('#').Last())
                            .UpdateDomElements(a => a.ClassName = null)
                            .Select(a => a.ToHtml())
                            .ToList();

        string CleanedTableOfContents = String.Join("\n", anchorElements);
        return CleanedTableOfContents;
    }

    /// <summary>
    /// Gets a section of the epub
    /// </summary>
    /// <param name="bookSection">The section of the epub to fetch, usually are chapters</param>
    /// <returns>Markdown string with the html body contents</returns>
    public static string? GetHtmlPage(string bookSection) {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        string? foundPage = Ebook.Resources.Html
            .Skip(1)
            .Select(h => context.OpenAsync(req => req.Content(h.TextContent)))
            .First(dom => dom.Result.IsCorrectPage(bookSection))
            .Result?.Body?.InnerHtml.Trim().ToString();

        return foundPage;
    }
}
