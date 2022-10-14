namespace Portal_Api.Services;
using EpubSharp;
using AngleSharp;
using AngleSharp.Html.Dom;

/// <summary>
/// Service wrapper to parse epub files
/// </summary>
public class EpubReaderService
{
    /// <summary>
    /// The parsed epub file
    /// </summary>
    private EpubBook Ebook { get; set; }

    /// <summary>
    /// Fetches the the ebook from the database and stores it in a EpubBook object
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    private void OpenEBook(int bookId) {
        string filePath = "";
        Ebook = EpubReader.Read(filePath);
    }

    /// <summary>
    /// Gets the parsed epub's title
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    /// <returns>The parsed epub's title</returns>
    public string GetTitle(int bookId) {
        OpenEBook(bookId);

        return Ebook.Title;
    }

    /// <summary>
    /// Gets the parsed epub's author(s)
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    /// <returns>The parsed epub's author(s)</returns>
    public string GetAuthors(int bookId) {
        OpenEBook(bookId);

        List<string> authors = Ebook.Authors.ToList();
        return authors.Count == 1 ? authors[0] : String.Join(", ", authors);
    }

    /// <summary>
    /// Gets the table of contents for the epub and removes
    /// unneeded info from the anchor elements
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    /// <returns>A string containing multiple anchor elements</returns>
    public string GetTableOfContents(int bookId) {
        OpenEBook(bookId);

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
    /// <param name="bookId">The book identifier</param>
    /// <param name="bookSection">The section of the epub to fetch, usually are chapters</param>
    /// <returns>Markdown string with the html body contents</returns>
    public string? GetHtmlPage(int bookId, string bookSection) {
        OpenEBook(bookId);

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
