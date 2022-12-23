using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;

using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    [HttpPost("signup")]
    public async Task<ActionResult<AuthenticatedResponse>> SignupAsync(User user)
    {
        try
        {
            User createdUser = await _userService.AddUserAsync(user);

            return Ok(CreateJwtToken(createdUser));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public ActionResult<AuthenticatedResponse> Login(LoginModel loginModel)
    {
        try
        {
            User fetchedUser = _userService.SignIn(loginModel);
            return Ok(CreateJwtToken(fetchedUser));
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
    [HttpDelete, Authorize]
    public async Task<ActionResult<bool>> DeleteUserAsync()
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(await _userService.DeleteUserAsync(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private AuthenticatedResponse CreateJwtToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@999"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:7042",
            audience: "https://localhost:7042",
            claims: new List<Claim>() { new Claim("UserId", user.UserId.ToString()) },
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return new AuthenticatedResponse
        {
            Token = tokenString,
            UserData = new User { Name = user.Name, Email = user.Email }
        };
    }
}
