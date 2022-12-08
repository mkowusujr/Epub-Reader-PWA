using EpubSharp;
using AngleSharp;

namespace PortalApi.Services;
/// <summary>
/// Service wrapper to parse epub files
/// </summary>
public class EpubReaderService : IEpubReaderService
{
    /// <inheritdoc/>
    public EpubBook ParsedEpubFile(string filename)
    {
        string epubStorageDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"EpubStorage\EpubFiles");
        string filePath = Path.Combine(epubStorageDirectory, filename);
        return EpubReader.Read(filePath);
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
    public string? GetHtmlPage(EpubBook? eBook, string fileName)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        string? foundChapterTextContent =
            eBook?.Resources.Html.First(chapter => chapter.FileName == fileName).TextContent;

        string? parsedHtmlContent = context.OpenAsync(req => req.Content(foundChapterTextContent)).Result?.Body
            ?.InnerHtml.Trim().ToString();

        return parsedHtmlContent;
    }
}
