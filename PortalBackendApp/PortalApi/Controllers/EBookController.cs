using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;

namespace PortalApi.Controllers;

[ApiController]
[Route("api/ebooks")]
public class EBookController : ControllerBase
{
    private readonly IEBookService _eBookService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="eBookService"></param>
    public EBookController(IEBookService eBookService) => _eBookService = eBookService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="eBookInputModel"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<EBook>? AddBookForUser(EBookInputModel eBookInputModel)
    {
        try
        {
            return Ok(_eBookService.AddEBookForUser(eBookInputModel));
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/ebooks/ebook/{eBookId}")]
    public ActionResult<EBook?> GetEBookForUser(int userId, int eBookId)
    {
        try
        {
            return Ok(_eBookService.GetEBookForUser(userId, eBookId));
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/ebooks")]
    public ActionResult<List<EBook>> GetEBooksForUser(int userId)
    {
        try
        {
            return Ok(_eBookService.GetEBooksForUser(userId));
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    [HttpDelete("users/user/{userId}/ebooks/ebook/{eBookId}")]
    public ActionResult<bool> DeleteEBookForUser(int userId, int eBookId)
    {
        try
        {
            return Ok(_eBookService.DeleteEBookForUser(userId, eBookId));
        }
        catch
        {
            return BadRequest();
        }
    }
}
