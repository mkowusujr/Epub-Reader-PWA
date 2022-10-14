namespace Portal_Api.Services;

public interface IEpubReaderService
{
    /// <summary>
    /// Fetches the the ebook from the database and stores it in a EpubBook object
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    public string GetTitle(int bookId);

    /// <summary>
    /// Gets the parsed epub's author(s)
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    /// <returns>The parsed epub's author(s)</returns>
    public string GetAuthors(int bookId);

    /// <summary>
    /// Gets the table of contents for the epub and removes
    /// unneeded info from the anchor elements
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    /// <returns>A string containing multiple anchor elements</returns>
    public string GetTableOfContents(int bookId);

    /// <summary>
    /// Gets a section of the epub
    /// </summary>
    /// <param name="bookId">The book identifier</param>
    /// <param name="bookSection">The section of the epub to fetch, usually are chapters</param>
    /// <returns>Markdown string with the html body contents</returns>
    public string? GetHtmlPage(int bookId, string bookSection);
}
