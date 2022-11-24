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
    public List<EpubChapter>? GetTableOfContents(EpubBook? eBook)
    {
        return eBook?.TableOfContents;
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
