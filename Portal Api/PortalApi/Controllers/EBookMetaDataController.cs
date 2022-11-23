using Microsoft.AspNetCore.Mvc;
using Portal_Api.Services;
using Portal_Api.Models;
namespace Portal_Api.Controllers;

[ApiController]
[Route("ebookmetadata")]
public class EBookMetaDataController : ControllerBase
{
    private readonly IEBookMetaDataService _eBookMetaDataService;
    private readonly ILogger<EBookMetaDataController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EBookMetaDataController"/> class.
    /// </summary>
    /// <param name="eBookMetaDataService">The e book meta data service.</param>
    public EBookMetaDataController(
        IEBookMetaDataService eBookMetaDataService,
        ILogger<EBookMetaDataController> logger
    )
    {
        _eBookMetaDataService = eBookMetaDataService;
        _logger = logger;
    }

    /// <summary>
    /// Adds the book meta data.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<EBookMetaData> AddBookMetaData() {
        IFormFile? eBookMetaData = Request.Form.Files.FirstOrDefault();
        if (eBookMetaData != null){
            return _eBookMetaDataService.AddBookMetaData(eBookMetaData);
        }
        return null;
    }

    /// <summary>
    /// Gets the e book meta data.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("book/{bookId}")]
    public ActionResult<EBookMetaData?> GetEBookMetaData(int bookId) =>
        _eBookMetaDataService.GetEBookMetaData(bookId);

    /// <summary>
    /// Gets the e book meta data list.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<EBookMetaData>> GetEBookMetaDataList() =>
        _eBookMetaDataService.GetEBookMetaDataList();

    /// <summary>
    /// Deletes the e book meta data.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpDelete("book/{bookId}")]
    public ActionResult<bool> DeleteEBookMetaData(int bookId) =>
        _eBookMetaDataService.DeleteEBookMetaData(bookId);
}
