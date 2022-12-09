using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class UserService : IUserService
{
    private readonly PortalDbContext _context;

    public UserService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public User AddUser(User user)
    {
        try
        {
            User createUser = _context.Users
                .Add(
                    new User
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password
                    }
                )
                .Entity;
            _context.SaveChanges();
            return createUser;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public bool DeleteUser(int userId)
    {
        try
        {
            User fetchedUser = _context.Users.First(u => u.UserId == userId);
            _context.Remove(fetchedUser);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public User GetUser(int userId)
    {
        try
        {
            return _context.Users.First(u => u.UserId == userId);
        }
        catch
        {
            throw new Exception();
        }
    }
}
