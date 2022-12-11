using EpubSharp;

namespace PortalApi.Services.Interfaces;

/// <summary>
///
/// </summary>
public interface IEpubReaderService
{
    /// <summary>
    /// Parses the epub file and stores it as a EpubBook object
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>The e book</returns>
    public EpubBook ParsedEpubFile(Stream epubFileStream);

    /// <summary>
    /// Gets the title of the epub
    /// </summary>
    /// <param name="eBook">The e book.</param>
    public string GetTitle(EpubBook eBook);

    /// <summary>
    /// Gets the authors.
    /// </summary>
    /// <param name="eBook">The e book.</param>
    /// <returns></returns>
    public string GetAuthors(EpubBook eBook);

    /// <summary>
    /// Gets the table of contents for the epub and removes
    /// unneeded info from the anchor elements
    /// </summary>
    /// <param name="eBook">The e book.</param>
    /// <returns>A string containing multiple anchor elements</returns>
    public List<EpubChapter> GetTableOfContents(EpubBook eBook);

    /// <summary>
    /// Gets the cover image of the ebook
    /// </summary>
    /// <param name="eBook">The e book.</param>
    /// <returns>The image data as a byte[]</returns>
    public byte[] GetCoverImage(EpubBook eBook);

    /// <summary>
    /// Gets a section of the epub
    /// </summary>
    /// <param name="eBook">The e book.</param>
    /// <param name="fileName">The filename of the page being fetched</param>
    /// <returns>Markdown string with the html body contents</returns>
    public string? GetHtmlPage(EpubBook eBook, string fileName);
}
