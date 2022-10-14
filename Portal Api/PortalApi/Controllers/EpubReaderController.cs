using Microsoft.AspNetCore.Mvc;
using EpubSharp;
using Portal_Api.Services;
namespace Portal_Api.Controllers;

[ApiController]
[Route("epubreader")]
public class EpubReaderController : ControllerBase
{
    private readonly IEpubReaderService _epubReaderService;
    private readonly IEBookMetaDataService _ebookMetaDataService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EpubReaderController"/> class.
    /// </summary>
    /// <param name="epubReaderService">The epub reader service.</param>
    /// <param name="ebookMetaDataService">The ebook meta data service.</param>
    public EpubReaderController(IEpubReaderService epubReaderService, IEBookMetaDataService ebookMetaDataService)
    {
        _epubReaderService = epubReaderService;
        _ebookMetaDataService = ebookMetaDataService;
    }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{bookId}/title")]
    public ActionResult<string?> GetTitle(int bookId)
        => _epubReaderService.GetTitle(GetEpubBook(bookId));

    /// <summary>
    /// Gets the authors.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{id}/authors")]
    public ActionResult<string?> GetAuthors(int bookId) =>
        _epubReaderService.GetAuthors(GetEpubBook(bookId));

    /// <summary>
    /// Gets the table of contents.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{id}/tableofcontents")]
    public ActionResult<string?> GetTableOfContents(int bookId) =>
        _epubReaderService.GetTableOfContents(GetEpubBook(bookId));

    /// <summary>
    /// Gets the page.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <returns></returns>
    [HttpGet("read-book/{id}/sections/{sectionName}")]
    public ActionResult<string?> GetPage(int bookId, string sectionName) =>
        _epubReaderService.GetHtmlPage(GetEpubBook(bookId), sectionName);

    /// <summary>
    /// Gets the epub book.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    private EpubBook? GetEpubBook(int bookId)
    {
        var eBookMetaData = _ebookMetaDataService.GetEBookMetaData(bookId);
        if (eBookMetaData != null)
        {
            return _epubReaderService.ParsedEpubFile(eBookMetaData.FilePath);
        }

        return null;
    }
}