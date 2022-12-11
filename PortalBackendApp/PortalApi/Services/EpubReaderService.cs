using EpubSharp;
using AngleSharp;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

/// <summary>
/// Service wrapper to parse epub files
/// </summary>
public class EpubReaderService : IEpubReaderService
{
    /// <inheritdoc/>
    public EpubBook ParsedEpubFile(Stream epubFileStream)
    {
        return EpubReader.Read(epubFileStream, leaveOpen: false);
    }

    /// <inheritdoc/>
    public string GetTitle(EpubBook eBook)
    {
        return eBook.Title;
    }

    /// <inheritdoc/>
    public string GetAuthors(EpubBook eBook)
    {
        List<string> authors = eBook.Authors.ToList();
        return authors.Count == 1 ? authors[0] : string.Join(", ", authors);
    }

    /// <inheritdoc/>
    public List<EpubChapter> GetTableOfContents(EpubBook eBook)
    {
        return eBook.TableOfContents;
    }

    /// <inheritdoc/>
    public byte[] GetCoverImage(EpubBook eBook)
    {
        return eBook.CoverImage;
    }

    /// <inheritdoc/>
    public string? GetHtmlPage(EpubBook eBook, string fileName)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        string? foundChapterTextContent = eBook?.Resources.Html
            .First(chapter => chapter.FileName == fileName)
            .TextContent;

        string? parsedHtmlContent = context
            .OpenAsync(req => req.Content(foundChapterTextContent))
            .Result?.Body?.InnerHtml.Trim()
            .ToString();

        return parsedHtmlContent;
    }
}
