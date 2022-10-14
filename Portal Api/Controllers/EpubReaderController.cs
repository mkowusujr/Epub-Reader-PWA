using Microsoft.AspNetCore.Mvc;
using Portal_Api.Services;
namespace Portal_Api.Controllers;

[ApiController]
[Route("epubreader")]
public class EpubReaderController : ControllerBase
{
    private readonly IEpubReaderService _epubReaderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EpubReaderController"/> class.
    /// </summary>
    /// <param name="epubReaderService">The epub reader service.</param>
    public EpubReaderController (IEpubReaderService epubReaderService)
    {
        _epubReaderService = epubReaderService;
    }

    /// <summary>
    /// Gets the title.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{bookId}/title")]
    public ActionResult<string> GetTitle(int bookId) => 
        _epubReaderService.GetTitle(bookId);

    /// <summary>
    /// Gets the authors.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{id}/authors")]
    public ActionResult<string> GetAuthors(int bookId) => 
        _epubReaderService.GetAuthors(bookId);

    /// <summary>
    /// Gets the table of contents.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{id}/tableofcontents")]
    public ActionResult<string> GetTableOfContents(int bookId) => 
        _epubReaderService.GetTableOfContents(bookId);

    /// <summary>
    /// Gets the page.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <returns></returns>
    [HttpGet("read-book/{id}/sections/{sectionName}")]
    public ActionResult<string?> GetPage(int bookId, string sectionName) => 
        _epubReaderService.GetHtmlPage(bookId, sectionName);
}