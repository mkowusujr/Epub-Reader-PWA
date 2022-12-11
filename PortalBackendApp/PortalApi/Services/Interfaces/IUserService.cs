using PortalApi.Models;

namespace PortalApi.Services.Interfaces;

/// <summary>
///
/// </summary>
public interface IUserService
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public Task<User> AddUserAsync(User user);

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<User?> GetUserAsync(int userId);

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<bool> DeleteUserAsync(int userId);
}
