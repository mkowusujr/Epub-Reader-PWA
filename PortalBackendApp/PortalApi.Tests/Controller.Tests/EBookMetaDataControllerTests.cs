using PortalApi.Controllers;
using PortalApi.Services.Interfaces;
using PortalApi.Models;

using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace PortalApi.Tests.Controller.Tests;

public class EBookMetaDataControllerTests
{
    readonly IEBookService eBookService = A.Fake<IEBookService>();

    readonly EBookController eBookController;

    public EBookMetaDataControllerTests() => eBookController = new EBookController(eBookService);

    [Fact]
    void TestGetEBookForUser()
    {
        EBook mockEBook = A.Fake<EBook>();
        A.CallTo(() => eBookService.GetEBookForUser(A<int>.Ignored, A<int>.Ignored))
            .Returns(mockEBook);

        var response = eBookController.GetEBookForUser(userId: 1, eBookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBook);
    }

    [Fact]
    void TestGetEBookMetaDataList()
    {
        List<EBook> mockEBooks = A.CollectionOfDummy<EBook>(5).ToList();
        A.CallTo(() => eBookService.GetEBooksForUser(A<int>.Ignored)).Returns(mockEBooks);

        var response = eBookController.GetEBooksForUser(userId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBooks);
    }

    [Fact]
    void TestDeleteEBookMetaData()
    {
        bool expectedResult = true;
        EBook mockEBook = A.Fake<EBook>();
        A.CallTo(() => eBookService.DeleteEBookForUser(A<int>.Ignored, A<int>.Ignored)).Returns(expectedResult);

        var response = eBookController.DeleteEBookForUser(userId: 1, eBookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(expectedResult);
    }

    [Fact]
    void TestAddBookMetaData()
    {
        EBookInputModel mockEBookInputModel = A.Fake<EBookInputModel>();
        EBook mockEBook = A.Fake<EBook>();
        A.CallTo(() => eBookService.AddEBookForUser(A<EBookInputModel>.Ignored)).Returns(mockEBook);

        var response = eBookController.AddBookForUser(mockEBookInputModel);
        var result = response?.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBook);
    }
}
