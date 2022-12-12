namespace PortalApi.Tests.Services.Tests;

public class CollectionServiceTests
{
    public PortalMockObjects mockObjects = new PortalMockObjects();

    [Fact]
    public async Task AddCollectionAsync_ShouldAddANewEmptyCollectionForAUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                var response = await collectionService.AddCollectionAsync(
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection"
                    )
                );

                response.UserId.Should().Be(dummyUser.UserId);
                response.User.Should().Be(dummyUser);
                response.EBooks.Count.Should().Be(0);
            }
        }
    }

    [Fact]
    public async Task AddCollectionAsync_ShouldAddANewNonEmptyCollectionForAUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                context.EBooks.AddRange(
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection>()
                    ),
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId, bookOption: 2)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection>()
                    ),
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId, bookOption: 3)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection>()
                    )
                );
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                var response = await collectionService.AddCollectionAsync(
                    mockObjects.GenerateNonEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection",
                        eBooks: dummyUser.EBooks
                    )
                );

                response.UserId.Should().Be(dummyUser.UserId);
                response.User.Should().Be(dummyUser);
                response.EBooks.Should().NotBeNull();
                response.EBooks.Count.Should().Be(3);
                foreach (EBook eBook in response.EBooks)
                {
                    eBook.Collections.Contains(response);
                }
            }
        }
    }

    [Fact]
    public async Task GetCollectionForUserAsync_ShouldFetchedTheSpecifiedEmptyCollectionForTheUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                Collection dummyCollection = context.Collections
                    .Add(
                        mockObjects.GenerateEmptyCollectionForUser(
                            userId: dummyUser.UserId,
                            collectionName: "Empty Collection"
                        )
                    )
                    .Entity;
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                Collection? response = await collectionService.GetCollectionForUserAsync(
                    userId: dummyUser.UserId,
                    collectionId: dummyCollection.CollectionId
                );

                response.Should().NotBeNull();
                response.CollectionId.Should().Be(dummyCollection.CollectionId);
                response.UserId.Should().Be(dummyUser.UserId);
                response.User.Should().Be(dummyUser);
                response.EBooks.Count.Should().Be(0);
            }
        }
    }

    [Fact]
    public async Task GetCollectionForUserAsync_ShouldFetchedTheSpecifiedNonEmptyCollectionForTheUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                context.EBooks.AddRange(
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection>()
                    ),
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId, bookOption: 2)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection>()
                    ),
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId, bookOption: 3)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection>()
                    )
                );
                context.SaveChanges();
                Collection dummyCollection = context.Collections
                    .Add(
                        mockObjects.GenerateNonEmptyCollectionForUser(
                            userId: dummyUser.UserId,
                            collectionName: "Empty Collection",
                            eBooks: dummyUser.EBooks
                        )
                    )
                    .Entity;
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                Collection? response = await collectionService.GetCollectionForUserAsync(
                    userId: dummyUser.UserId,
                    collectionId: dummyCollection.CollectionId
                );

                response.Should().NotBeNull();
                response.CollectionId.Should().Be(dummyCollection.CollectionId);
                response.UserId.Should().Be(dummyUser.UserId);
                response.User.Should().Be(dummyUser);
                response.EBooks.Count.Should().Be(3);
            }
        }
    }

    [Fact]
    public async Task GetCollectionForUserAsync_ShouldThrowError_IfCollectionDoesntExist()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                Func<Task> act = async () =>
                    await collectionService.GetCollectionForUserAsync(
                        userId: dummyUser.UserId,
                        collectionId: 404
                    );

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task GetCollectionsForUserAsync_ShouldGetAllTheCollectionsForTheSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                context.Collections.AddRange(
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection #1"
                    ),
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection #2"
                    ),
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection #3"
                    )
                );
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                List<Collection>? response = await collectionService.GetCollectionsForUserAsync(
                    dummyUser.UserId
                );

                response.Should().NotBeNull();
                response.Count.Should().Be(3);
                foreach (Collection collection in response)
                {
                    collection.User.Should().Be(dummyUser);
                    collection.UserId.Should().Be(dummyUser.UserId);
                }
            }
        }
    }

    [Fact]
    public async Task GetCollectionsForUserAsync_ShouldThrowError_WhenUserDoesntExist()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                Func<Task> act = async () =>
                    await collectionService.GetCollectionsForUserAsync(userId: 404);

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task AddEBookToCollectionForUserAsync_ShouldAddAnEBookToSpecifiedCollectionForTheSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                context.Collections.Add(
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection #1"
                    )
                );
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
                CollectionService collectionService = new CollectionService(context);

                bool response = await collectionService.AddEBookToCollectionForUserAsync(
                    userId: dummyUser.UserId,
                    ebookId: dummyEBook.EBookId,
                    collectionId: dummyUser.Collections.First().CollectionId
                );

                response.Should().BeTrue();
                dummyUser.Collections.Should().NotBeNull();
                dummyUser.Collections.First().Should().NotBeNull();
                dummyEBook.Collections.Count.Should().Be(1);
                dummyEBook.Collections.Should().Contain(dummyUser.Collections.First());
                dummyEBook.Collections
                    .First()
                    .CollectionId.Should()
                    .Be(dummyUser.Collections.First().CollectionId);
                dummyEBook.User.Should().Be(dummyUser);
            }
        }
    }

    [Fact]
    public async Task AddEBookToCollectionForUserAsync_ShouldThrowError_IfUserDoesntExist()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                CollectionService collectionService = new CollectionService(context);

                Func<Task> act = async () =>
                    await collectionService.AddEBookToCollectionForUserAsync(
                        userId: 404,
                        ebookId: 404,
                        collectionId: 404
                    );

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task RemoveEBookFromCollectionAsync_ShouldRemoveAnEBookToSpecifiedCollectionForTheSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                context.Collections.Add(
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection #1"
                    )
                );
                context.SaveChanges();
                EBook dummyEBook = context.EBooks
                    .Add(
                        new EBook(
                            epubFileStream: mockObjects
                                .GenerateEBookForUser(dummyUser.UserId)
                                .EpubFile.OpenReadStream(),
                            userId: dummyUser.UserId,
                            collections: new List<Collection> { dummyUser.Collections.First() }
                        )
                    )
                    .Entity;
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                bool response = await collectionService.RemoveEBookFromCollectionAsync(
                    userId: dummyUser.UserId,
                    ebookId: dummyEBook.EBookId,
                    collectionId: dummyUser.Collections.First().CollectionId
                );

                response.Should().BeTrue();
                dummyUser.Collections.Should().NotBeNull();
                dummyUser.Collections.First().Should().NotBeNull();
                dummyUser.Collections.First().EBooks.Should().BeEmpty();
                dummyEBook.Collections.Should().BeEmpty();
            }
        }
    }

    [Fact]
    public async Task RemoveEBookFromCollectionAsync_ShouldThrowError_IfUserDoesntExist()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                CollectionService collectionService = new CollectionService(context);

                Func<Task> act = async () =>
                    await collectionService.RemoveEBookFromCollectionAsync(
                        userId: 404,
                        ebookId: 404,
                        collectionId: 404
                    );

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }

    [Fact]
    public async Task GetEBooksInCollectionForUserAsync_ShouldGetAllTheEBooksFromTheSpecifiedCollectionFromTheSpecifiedUser_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                context.Collections.Add(
                    mockObjects.GenerateEmptyCollectionForUser(
                        userId: dummyUser.UserId,
                        collectionName: "Empty Collection #1"
                    )
                );
                context.SaveChanges();
                context.EBooks.AddRange(
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection> { dummyUser.Collections.First() }
                    ),
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId, bookOption: 2)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection> { dummyUser.Collections.First() }
                    ),
                    new EBook(
                        epubFileStream: mockObjects
                            .GenerateEBookForUser(dummyUser.UserId, bookOption: 3)
                            .EpubFile.OpenReadStream(),
                        userId: dummyUser.UserId,
                        collections: new List<Collection> { dummyUser.Collections.First() }
                    )
                );
                context.SaveChanges();
                CollectionService collectionService = new CollectionService(context);

                List<EBook> response = await collectionService.GetEBooksInCollectionForUserAsync(
                    userId: dummyUser.UserId,
                    collectionId: dummyUser.Collections.First().CollectionId
                );

                dummyUser.Collections.Should().NotBeEmpty();
                dummyUser.Collections.First().EBooks.Count().Should().Be(3);
                response.Count.Should().Be(3);
                foreach (EBook eBook in response)
                {
                    eBook.UserId.Should().Be(dummyUser.UserId);
                    eBook.User.Should().Be(dummyUser);
                    eBook.Collections.Should().Contain(dummyUser.Collections.First());
                }
            }
        }
    }

    [Fact]
    public async Task GetEBooksInCollectionForUserAsync_ShouldThrowError_IfUserDoesntExist()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                CollectionService collectionService = new CollectionService(context);

                Func<Task> act = async () =>
                    await collectionService.GetEBooksInCollectionForUserAsync(
                        userId: 404,
                        collectionId: 404
                    );

                await act.Should().ThrowAsync<Exception>();
            }
        }
    }
}
