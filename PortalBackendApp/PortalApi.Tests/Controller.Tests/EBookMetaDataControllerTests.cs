using PortalApi.Controllers;
using PortalApi.Services;
using PortalApi.Models;

using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PortalApi.Tests.Controller.Tests;
public class EBookMetaDataControllerTests
{
    readonly IEBookMetaDataService eBookMetaDataService = A.Fake<IEBookMetaDataService>();

    readonly EBookMetaDataController eBookMetaDataController;

    public EBookMetaDataControllerTests()
    {
        eBookMetaDataController = new EBookMetaDataController(eBookMetaDataService);
    }

    [Fact]
    void TestGetEBookMetaData()
    {
        EBookMetaData mockEBookMetaData = A.Fake<EBookMetaData>();
        A.CallTo(() => eBookMetaDataService.GetEBookMetaData(A<int>.Ignored))
            .Returns(mockEBookMetaData);

        var response = eBookMetaDataController.GetEBookMetaData(bookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBookMetaData);
    }

    [Fact]
    void TestGetEBookMetaDataList()
    {
        List<EBookMetaData> mockEBookMetaDatas = A.CollectionOfDummy<EBookMetaData>(5).ToList();
        A.CallTo(() => eBookMetaDataService.GetEBookMetaDataList())
            .Returns(mockEBookMetaDatas);

        var response = eBookMetaDataController.GetEBookMetaDataList();
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBookMetaDatas);
    }

    [Fact]
    void TestDeleteEBookMetaData()
    {
        bool expectedResult = true;
        EBookMetaData mockEBookMetaData = A.Fake<EBookMetaData>();
        A.CallTo(() => eBookMetaDataService.DeleteEBookMetaData(A<int>.Ignored))
            .Returns(expectedResult);

        var response = eBookMetaDataController.DeleteEBookMetaData(bookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(expectedResult);
    }

    [Fact]
    void TestAddBookMetaData()
    {
        IFormFile mockFile = A.Fake<IFormFile>();
        EBookMetaData mockEBookMetaData = A.Fake<EBookMetaData>();
        A.CallTo(() => eBookMetaDataService.AddBookMetaData(A<IFormFile>.Ignored))
            .Returns(mockEBookMetaData);

        var response = eBookMetaDataController.AddBookMetaData(mockFile);
        var result = response?.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBookMetaData);
    }
}
