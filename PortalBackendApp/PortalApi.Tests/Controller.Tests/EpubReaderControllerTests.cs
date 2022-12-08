using PortalApi.Controllers;
using PortalApi.Services;

using EpubSharp;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace PortalApi.Tests.Controllers;
public class EpubReaderControllerTests
{
    readonly IEpubReaderService epubReaderService = A.Fake<IEpubReaderService>();
    readonly IEBookMetaDataService eBookMetaDataService = A.Fake<IEBookMetaDataService>();
    readonly EpubReaderController epubReaderController;

    public EpubReaderControllerTests()
    {
        epubReaderController = new EpubReaderController(
            ebookMetaDataService: eBookMetaDataService,
            epubReaderService: epubReaderService
        );
    }

    [Fact]
    void TestGetTitle()
    {
        string mockEBookTitle = "Mock EBook Title";
        A.CallTo(() => epubReaderService.GetTitle(A<EpubBook>.Ignored)).Returns(mockEBookTitle);

        var response = epubReaderController.GetTitle(bookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBookTitle);
    }

    [Fact]
    void TestGetAuthors()
    {
        string mockEBookAuthors = "Mock EBook Author";
        A.CallTo(() => epubReaderService.GetAuthors(A<EpubBook>.Ignored)).Returns(mockEBookAuthors);

        var response = epubReaderController.GetAuthors(bookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(mockEBookAuthors);
    }

    [Fact]
    void TestGetTableOfContents()
    {
        List<EpubChapter> chapterNames = A.CollectionOfDummy<EpubChapter>(5).ToList();
        A.CallTo(() => epubReaderService.GetTableOfContents(A<EpubBook>.Ignored))
            .Returns(chapterNames);

        var response = epubReaderController.GetTableOfContents(bookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(chapterNames);
    }

    [Fact]
    void TestGetCoverImage()
    {
        byte[] eBookCoverImageData = A.CollectionOfDummy<byte>(10).ToArray();
        A.CallTo(() => epubReaderService.GetCoverImage(A<EpubBook>.Ignored))
            .Returns(eBookCoverImageData);

        var response = epubReaderController.GetCoverImage(bookId: 1);
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(eBookCoverImageData);
    }

    [Fact]
    void TestGetPage()
    {
        string htmlPageTextContent = "<h1>Text Content</h1>";
        A.CallTo(() => epubReaderService.GetHtmlPage(A<EpubBook>.Ignored, A<String>.Ignored))
            .Returns(htmlPageTextContent);

        var response = epubReaderController.GetPage(bookId: 1, fileName: "mock ebok chapter");
        var result = response.Result as OkObjectResult;

        result?.Value.Should().Be(htmlPageTextContent);
    }
}
