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
    public User AddUser(User user);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public User GetUser(int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool DeleteUser(int userId);
}
