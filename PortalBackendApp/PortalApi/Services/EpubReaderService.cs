using EpubSharp;
using AngleSharp;
using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

/// <summary>
/// Service wrapper to parse epub files
/// </summary>
public class EpubReaderService : IEpubReaderService
{
    /// <inheritdoc/>
    public EpubBook ParseEpubFile(Stream epubFileStream)
    {
        try
        {
            return EpubReader.Read(epubFileStream, leaveOpen: false);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public string GetTitle(EpubBook eBook)
    {
        try
        {
            return eBook.Title;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public string GetAuthors(EpubBook eBook)
    {
        try
        {
            List<string> authors = eBook.Authors.ToList();
            return authors.Count == 1 ? authors[0] : string.Join(", ", authors);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public List<EpubChapter> GetTableOfContents(EBook eBook)
    {
        try
        {
            Stream epubFileStream = new MemoryStream(eBook.EpubFile);
            return ParseEpubFile(epubFileStream).TableOfContents;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public byte[] GetCoverImage(EpubBook eBook)
    {
        return eBook.CoverImage;
    }

    /// <inheritdoc/>
    public string? GetHtmlPage(EBook eBook, string fileName)
    {
        Stream epubFileStream = new MemoryStream(eBook.EpubFile);
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        string? foundChapterTextContent = ParseEpubFile(epubFileStream)?.Resources.Html
            .First(chapter => chapter.FileName == fileName)
            .TextContent;

        string? parsedHtmlContent = context
            .OpenAsync(req => req.Content(foundChapterTextContent))
            .Result?.Body?.InnerHtml.Trim()
            .ToString();

        return parsedHtmlContent;
    }
}
