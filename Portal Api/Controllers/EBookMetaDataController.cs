using Microsoft.AspNetCore.Mvc;
using Portal_Api.Services;
using Portal_Api.Models;
namespace Portal_Api.Controllers;

[ApiController]
[Route("ebookmetadata")]
public class EBookMetaDataController : ControllerBase
{
    private readonly IEBookMetaDataService _eBookMetaDataService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EBookMetaDataController"/> class.
    /// </summary>
    /// <param name="eBookMetaDataService">The e book meta data service.</param>
    public EBookMetaDataController(IEBookMetaDataService eBookMetaDataService)
    {
        _eBookMetaDataService = eBookMetaDataService;
    }

    /// <summary>
    /// Adds the book meta data.
    /// </summary>
    /// <param name="eBookMetaData">The e book meta data.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<EBookMetaData> AddBookMetaData(EBookMetaData eBookMetaData) =>
        _eBookMetaDataService.AddBookMetaData(eBookMetaData);

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
