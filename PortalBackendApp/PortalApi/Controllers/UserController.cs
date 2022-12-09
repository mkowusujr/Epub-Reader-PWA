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
    public ActionResult<User> AddUser(User user)
    {
        try
        {
            return Ok(_userService.AddUser(user));
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
    [HttpGet("{userId}")]
    public ActionResult<User> GetUser(int userId)
    {
        try
        {
            return Ok(_userService.GetUser(userId));
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
    [HttpDelete("{userId}")]
    public ActionResult<bool> DeleteUser(int userId)
    {
        try
        {
            return Ok(_userService.DeleteUser(userId));
        }
        catch
        {
            return BadRequest();
        }
    }
}
