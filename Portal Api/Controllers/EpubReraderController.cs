using Microsoft.AspNetCore.Mvc;
using Portal_Api.Services;
namespace Portal_Api.Controllers;

[ApiController]
[Route("epubreader")]
public class EpubReaderController : ControllerBase
{
    private EpubReaderService _epubReaderService;
    public EpubReaderController (EpubReaderService epubReaderService)
    {
        _epubReaderService = epubReaderService;
    }

    [HttpGet("read-book/{bookId}/title")]
    public ActionResult<string> GetTitle(int bookId) => 
        _epubReaderService.GetTitle(bookId);

    [HttpGet("read-book/{id}/authors")]
    public ActionResult<string> GetAuthors(int bookId) => 
        _epubReaderService.GetAuthors(bookId);

    [HttpGet("read-book/{id}/tableofcontents")]
    public ActionResult<string> GetTableOfContents(int bookId) => 
        _epubReaderService.GetTableOfContents(bookId);

    [HttpGet("read-book/{id}/sections/{sectionName}")]
    public ActionResult<string?> GetPage(int bookId, string sectionName) => 
        _epubReaderService.GetHtmlPage(bookId, sectionName);
}