namespace PortalApi.Tests.Services.Tests;

public class UserServiceTests
{
    public PortalMockObjects mockObjects = new PortalMockObjects();

    [Fact]
    public async Task GetUserAsync_ShouldFetchTheSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                UserService userService = new UserService(context);

                context.Users.Add(mockObjects.mockUserJohnDoe);
                context.SaveChanges();

                User? response = await userService.GetUserAsync(userId: 1);

                response.Should().NotBeNull();
                response.UserId.Should().Be(1);
                response.Should().Be(mockObjects.mockUserJohnDoe);
            }
        }
    }

    [Fact]
    public async Task GetUserAsync_ShouldThrowError_WithInvalidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                UserService userService = new UserService(context);

                Func<Task> act = async () => await userService.GetUserAsync(userId: 1);
                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task AddUserAsync_ShouldAddANewUserToTheDatabase_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                UserService userService = new UserService(context);

                User? response = await userService.AddUserAsync(mockObjects.mockUserJaneDoe);

                response.Should().NotBeNull();
                response.UserId.Should().Be(1);
                response.Should().Be(mockObjects.mockUserJaneDoe);
            }
        }
    }

    [Fact]
    public async Task AddUserAsync_ShouldThrowError_WithInalidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                UserService userService = new UserService(context);

                Func<Task> act = async () => await userService.AddUserAsync(new Object() as User);

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldDeleteAUserFromTheDatabase_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                UserService userService = new UserService(context);

                context.Users.Add(mockObjects.mockUserJohnDoe);
                context.SaveChanges();

                bool response = await userService.DeleteUserAsync(userId: 1);

                response.Should().BeTrue();
            }
        }
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldThrowError_WithInalidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                UserService userService = new UserService(context);

                context.Users.Add(mockObjects.mockUserJohnDoe);
                context.SaveChanges();

                Func<Task> act = async () => await userService.DeleteUserAsync(userId: 2);

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }
}
