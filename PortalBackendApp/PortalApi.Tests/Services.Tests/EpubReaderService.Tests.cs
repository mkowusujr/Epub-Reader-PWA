using EpubSharp;
using Newtonsoft.Json;

namespace PortalApi.Tests.Services.Tests;

public class EReaderSharpServiceTests
{
    public PortalMockObjects mockObjects = new PortalMockObjects();

    [Fact]
    public void ParseEpubFile_ShouldReturnAnEpubBook_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EpubReaderService epubReaderService = new EpubReaderService();

                EBookInputModel dummyEBookInputModel = mockObjects.GenerateEBookInputModelForUser(
                    userId: dummyUser.UserId
                );

                EpubBook response = epubReaderService.ParseEpubFile(
                    dummyEBookInputModel.EpubFile.OpenReadStream()
                );
            }
        }
    }

    [Fact]
    public void ParseEpubFile_ShouldThrowError_OnInvalidInput()
    {
        EpubReaderService epubReaderService = new EpubReaderService();

        Action act = () => epubReaderService.ParseEpubFile(epubFileStream: new Object() as Stream);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void GetTitle_ShouldReturnAnEpubTitle_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EpubReaderService epubReaderService = new EpubReaderService();

                EBookInputModel dummyEBookInputModel = mockObjects.GenerateEBookInputModelForUser(
                    userId: dummyUser.UserId
                );

                EpubBook epubBook = epubReaderService.ParseEpubFile(
                    dummyEBookInputModel.EpubFile.OpenReadStream()
                );

                string response = epubReaderService.GetTitle(epubBook);

                response.Should().Be("Frankenstein; Or, The Modern Prometheus");
            }
        }
    }

    [Fact]
    public void GetTitle_ShouldThrowError_OnInvalidInput()
    {
        EpubReaderService epubReaderService = new EpubReaderService();

        Action act = () => epubReaderService.GetTitle(new Object() as EpubBook);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void GetAuthors_ShouldReturnAnEpubAuthor_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EpubReaderService epubReaderService = new EpubReaderService();

                EBookInputModel dummyEBookInputModel = mockObjects.GenerateEBookInputModelForUser(
                    userId: dummyUser.UserId
                );

                EpubBook epubBook = epubReaderService.ParseEpubFile(
                    dummyEBookInputModel.EpubFile.OpenReadStream()
                );

                string response = epubReaderService.GetAuthors(epubBook);

                response.Should().Be("Mary Wollstonecraft Shelley");
            }
        }
    }

    [Fact]
    public void GetAuthors_ShouldThrowError_OnInvalidInput()
    {
        EpubReaderService epubReaderService = new EpubReaderService();

        Action act = () => epubReaderService.GetAuthors(new Object() as EpubBook);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void GetCoverImage_ShouldReturnAnEpubCoverImage_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EpubReaderService epubReaderService = new EpubReaderService();

                EBookInputModel dummyEBookInputModel = mockObjects.GenerateEBookInputModelForUser(
                    userId: dummyUser.UserId
                );

                EpubBook epubBook = epubReaderService.ParseEpubFile(
                    dummyEBookInputModel.EpubFile.OpenReadStream()
                );

                byte[] response = epubReaderService.GetCoverImage(epubBook);

                response.Should().NotBeNull();
            }
        }
    }

    [Fact]
    public void GetCoverImage_ShouldThrowError_OnInvalidInput()
    {
        EpubReaderService epubReaderService = new EpubReaderService();

        Action act = () => epubReaderService.GetCoverImage(new Object() as EpubBook);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void GetTableOfContents_ShouldReturnAnEpubAuthor_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EpubReaderService epubReaderService = new EpubReaderService();
                EBook dummyEBook = context.EBooks
                    .Add(
                        new EBook(
                            epubFileStream: mockObjects
                                .GenerateEBookInputModelForUser(dummyUser.UserId)
                                .EpubFile.OpenReadStream(),
                            userId: dummyUser.UserId,
                            collections: new List<Collection>()
                        )
                    )
                    .Entity;
                context.SaveChanges();

                List<EpubChapter> response = epubReaderService.GetTableOfContents(dummyEBook);

                response.Count.Should().Be(33);
            }
        }
    }

    [Fact]
    public void GetTableOfContents_ShouldThrowError_OnInvalidInput()
    {
        EpubReaderService epubReaderService = new EpubReaderService();

        Action act = () => epubReaderService.GetTableOfContents(new Object() as EBook);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void GetHtmlPage_ShouldReturnAnEpubHtmlPage_WithValidInput()
    {
        using (var factory = new PortalDbContextFactory())
        {
            using (var context = factory.CreateContext())
            {
                context.Database.EnsureCreated();
                User dummyUser = context.Users.Add(mockObjects.mockUserJohnDoe).Entity;
                context.SaveChanges();
                EpubReaderService epubReaderService = new EpubReaderService();
                EBook dummyEBook = context.EBooks
                    .Add(
                        new EBook(
                            epubFileStream: mockObjects
                                .GenerateEBookInputModelForUser(dummyUser.UserId)
                                .EpubFile.OpenReadStream(),
                            userId: dummyUser.UserId,
                            collections: new List<Collection>()
                        )
                    )
                    .Entity;
                context.SaveChanges();

                string? letter1 = epubReaderService.GetHtmlPage(
                    dummyEBook,
                    "7034958079997369471_84-h-1.htm.html"
                );
                string? letter2 = epubReaderService.GetHtmlPage(
                    dummyEBook,
                    "7034958079997369471_84-h-2.htm.html"
                );
                string? chapter1 = epubReaderService.GetHtmlPage(
                    dummyEBook,
                    "7034958079997369471_84-h-5.htm.html"
                );
                string? chapter11 = epubReaderService.GetHtmlPage(
                    dummyEBook,
                    "7034958079997369471_84-h-15.htm.html"
                );
                string? chapter14 = epubReaderService.GetHtmlPage(
                    dummyEBook,
                    "7034958079997369471_84-h-18.htm.html"
                );

                letter1.Should().NotBeNull().And.Contain("Letter 1");
                letter2.Should().NotBeNull().And.Contain("Letter 2");
                chapter1.Should().NotBeNull().And.Contain("Chapter 1");
                chapter11.Should().NotBeNull().And.Contain("Chapter 1");
                chapter14.Should().NotBeNull().And.Contain("Chapter 1");
            }
        }
    }

    [Fact]
    public void GetHtmlPage_ShouldThrowError_OnInvalidInput()
    {
        EpubReaderService epubReaderService = new EpubReaderService();

        Action act = () => epubReaderService.GetHtmlPage(new Object() as EBook, fileName: "fakeFile");

        act.Should().Throw<Exception>();
    }
}
