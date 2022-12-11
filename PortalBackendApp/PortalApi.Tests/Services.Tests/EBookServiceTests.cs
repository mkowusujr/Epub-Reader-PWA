namespace PortalApi.Tests.Services.Tests;

public class EBookServiceTests
{
    public PortalMockObjects mockObjects = new PortalMockObjects();

    [Fact]
    public async Task AddEBookForUserAsync_ShouldAddEBookForSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EBookService eBookService = new EBookService(context);

                EBook? response = await eBookService.AddEBookForUserAsync(
                    mockObjects.GenerateEBookForUser(dummyUser.UserId)
                );

                response.Should().NotBeNull();
                response.EBookId.Should().Be(1);
                response.UserId.Should().Be(dummyUser.UserId);
            }
        }
    }

    [Fact]
    public async Task AddEBookForUserAsync_ShouldThrowError_IfUserInEBookInputModelDoesntExist()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EBookService eBookService = new EBookService(context);

                Func<Task> act = async () =>
                    await eBookService.AddEBookForUserAsync(
                        mockObjects.GenerateEBookForUser(userId: 404)
                    );

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task GetEBookForUserAsync_ShouldFetchForSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EBook dummyEBook = context.EBooks
                    .Add(
                        new EBook(
                            epubFileStream: mockObjects
                                .GenerateEBookForUser(dummyUser.UserId)
                                .EpubFile.OpenReadStream(),
                            userId: dummyUser.UserId,
                            collections: new List<Collection>()
                        )
                    )
                    .Entity;
                context.SaveChanges();
                EBookService eBookService = new EBookService(context);

                EBook? response = await eBookService.GetEBookForUserAsync(
                    userId: dummyUser.UserId,
                    eBookId: dummyEBook.EBookId
                );

                response.Should().NotBeNull();
                response.EBookId.Should().Be(dummyEBook.EBookId);
                response.UserId.Should().Be(dummyUser.UserId);
            }
        }
    }

    [Fact]
    public async Task GetEBookForUserAsync_ShouldThowError_WhenUserDoesntOwnEBook()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();

                EBookService eBookService = new EBookService(context);

                Func<Task> act = async () =>
                    await eBookService.GetEBookForUserAsync(userId: dummyUser.UserId, eBookId: 404);

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }
}
