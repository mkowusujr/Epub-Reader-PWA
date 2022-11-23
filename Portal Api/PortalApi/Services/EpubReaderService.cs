namespace Portal_Api.Services;
using EpubSharp;
using AngleSharp;
using AngleSharp.Html.Dom;

/// <summary>
/// Service wrapper to parse epub files
/// </summary>
public class EpubReaderService : IEpubReaderService
{
    /// <inheritdoc/>
    public EpubBook ParsedEpubFile(string filename)
    {
        return EpubReader.Read(filename);
    }

    /// <inheritdoc/>
    public string? GetTitle(EpubBook? eBook)
    {
        return eBook?.Title;
    }

    /// <inheritdoc/>
    public string? GetAuthors(EpubBook? eBook)
    {
        if (eBook != null)
        {
            List<string> authors = eBook.Authors.ToList();
            return authors.Count == 1 ? authors[0] : string.Join(", ", authors);
        }

        return null;
    }

    /// <inheritdoc/>
    public string? GetTableOfContents(EpubBook? eBook)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        using var parsedDom = context.OpenAsync(req => req.Content(eBook?.Resources.Html.ElementAt(0).TextContent))
            .Result;

        var anchorElements = parsedDom.QuerySelectorAll("a")
            .OfType<IHtmlAnchorElement>()
            .UpdateDomElements(a => a.Href = a.Href.Split('#').Last())
            .UpdateDomElements(a => a.ClassName = null)
            .Select(a => a.ToHtml())
            .ToList();

        var cleanedTableOfContents = string.Join("\n", anchorElements);
        return cleanedTableOfContents;
    }

    /// <inheritdoc/>
    public byte[]? GetCoverImage(EpubBook? eBook)
    {
        return eBook?.CoverImage;
    }

    /// <inheritdoc/>
    public string? GetHtmlPage(EpubBook? eBook, string bookSection)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        string? foundPage = eBook?.Resources.Html
            .Skip(1)
            .Select(h => context.OpenAsync(req => req.Content(h.TextContent)))
            .First(dom => dom.Result.IsCorrectPage(bookSection))
            .Result?.Body?.InnerHtml.Trim().ToString();

        return foundPage;
    }
}
