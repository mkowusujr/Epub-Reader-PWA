using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;
using Microsoft.AspNetCore.Authorization;

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
    [HttpPost, Authorize]
    public async Task<ActionResult<EBook>?> AddBookForUser(EBookInputModel eBookInputModel)
    {
        try
        {
            EBook createdEBook = await _eBookService.AddEBookForUserAsync(eBookInputModel);
            return CreatedAtAction(
                nameof(GetEBookForUserAsync),
                new { eBookId = createdEBook.EBookId },
                createdEBook
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    [HttpGet("ebook/{eBookId}"), Authorize]
    public async Task<ActionResult<EBook?>> GetEBookForUserAsync(int eBookId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            EBook? fetchedEBook = await _eBookService.GetEBookForUserAsync(userId, eBookId);
            return fetchedEBook != null ? Ok(fetchedEBook) : NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet, Authorize]
    public async Task<ActionResult<List<EBook>>> GetEBooksForUser()
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(await _eBookService.GetEBooksForUserAsync(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    [HttpDelete("ebook/{eBookId}"), Authorize]
    public async Task<ActionResult<bool>> DeleteEBookForUserAsync(int eBookId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(await _eBookService.DeleteEBookForUserAsync(userId, eBookId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
