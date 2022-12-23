using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class UserService : IUserService
{
    private readonly PortalDbContext _context;

    public UserService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            User createdUser = _context.Users
                .Add(new User(email: user.Email, name: user.Name, password: user.Password))
                .Entity;
            await _context.SaveChangesAsync();
            return createdUser;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteUserAsync(int userId)
    {
        try
        {
            User? fetchedUser = await _context.Users.FindAsync(userId);
            if (fetchedUser != null)
            {
                _context.Remove(fetchedUser);
                await _context.SaveChangesAsync();
                return true;
            }
            throw new Exception($"User {userId} doesn't exist");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public User SignIn(LoginModel loginModel)
    {
        try
        {
            User? fetechedUser = _context.Users.First(user => user.Email.Equals(loginModel.Email));
            if (fetechedUser == null)
            {
                throw new Exception($"User with email {loginModel.Email} doesn't exist");
            }
            if (!fetechedUser.Password.Equals(loginModel.Password))
            {
                throw new Exception("Incorrect password");
            }
            return fetechedUser;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
