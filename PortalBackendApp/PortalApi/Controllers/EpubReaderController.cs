using Microsoft.AspNetCore.Mvc;
using EpubSharp;
using PortalApi.Services;

namespace PortalApi.Controllers;
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
        => Ok(_epubReaderService.GetTitle(GetEpubBook(bookId)));

    /// <summary>
    /// Gets the authors.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{bookId}/authors")]
    public ActionResult<string?> GetAuthors(int bookId) =>
        Ok(_epubReaderService.GetAuthors(GetEpubBook(bookId)));

    /// <summary>
    /// Gets the table of contents.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{bookId}/tableofcontents")]
    public ActionResult<List<EpubChapter>?> GetTableOfContents(int bookId) =>
        Ok(_epubReaderService.GetTableOfContents(GetEpubBook(bookId)));

    /// <summary>
    /// Gets the cover page
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("read-book/{bookId}/coverimage")]
    public ActionResult<byte[]?> GetCoverImage(int bookId) => 
        _epubReaderService.GetCoverImage(GetEpubBook(bookId));

    /// <summary>
    /// Gets the page.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <param name="fileName">The filename of the page being fetched.</param>
    /// <returns></returns>
    [HttpGet("read-book/{bookId}/sections/{fileName}")]
    public ActionResult<string?> GetPage(int bookId, string fileName) =>
        _epubReaderService.GetHtmlPage(GetEpubBook(bookId), fileName);

    /// <summary>
    /// Gets the epub book.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    private EpubBook? GetEpubBook(int bookId)
    {
        var eBookMetaData = _ebookMetaDataService.GetEBookMetaData(bookId);
        if (eBookMetaData != null && eBookMetaData.FileName != null)
        {
            return _epubReaderService.ParsedEpubFile(eBookMetaData.FileName);
        }

        return null;
    }
}