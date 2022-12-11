using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;

namespace PortalApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService) => _userService = userService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<User>> AddUserAsync(User user)
    {
        try
        {
            User createdUser = await _userService.AddUserAsync(user);
            return CreatedAtAction(
                nameof(GetUserAsync),
                new { userId = createdUser.UserId },
                createdUser
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
    /// <returns></returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<User>> GetUserAsync(int userId)
    {
        try
        {
            User? fetchedUser = await _userService.GetUserAsync(userId);
            return fetchedUser != null ? Ok(fetchedUser) : NotFound();
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
    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> DeleteUserAsync(int userId)
    {
        try
        {
            return Ok(await _userService.DeleteUserAsync(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
