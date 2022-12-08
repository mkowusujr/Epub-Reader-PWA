using Microsoft.AspNetCore.Mvc;
using PortalApi.Services;
using PortalApi.Models;

namespace PortalApi.Controllers;
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
    /// <returns></returns>
    [HttpPost]
    public ActionResult<EBookMetaData>? AddBookMetaData([FromForm] IFormFile? eBookFile) =>
        eBookFile != null ? Ok(_eBookMetaDataService.AddBookMetaData(eBookFile)) : BadRequest();

    /// <summary>
    /// Gets the e book meta data.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpGet("book/{bookId}")]
    public ActionResult<EBookMetaData?> GetEBookMetaData(int bookId) =>
        _eBookMetaDataService.GetEBookMetaData(bookId) != null
            ? Ok(_eBookMetaDataService.GetEBookMetaData(bookId))
            : BadRequest();

    /// <summary>
    /// Gets the e book meta data list.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<EBookMetaData>> GetEBookMetaDataList() =>
        Ok(_eBookMetaDataService.GetEBookMetaDataList());

    /// <summary>
    /// Deletes the e book meta data.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    [HttpDelete("book/{bookId}")]
    public ActionResult<bool> DeleteEBookMetaData(int bookId) =>
        _eBookMetaDataService.DeleteEBookMetaData(bookId) ? Ok() : BadRequest();
}
