using EpubSharp;
using FluentAssertions;
using Portal_Api.Services;

namespace PortalApi.Tests.Services.Tests;

public class EReaderSharpServiceTests
{
    private readonly string resourcesDirectory =
        Path.Combine(Directory.GetCurrentDirectory(), @"Services.Tests\Resources\");

    private readonly EpubReaderService _epubReaderService = new();
    private EpubBook eBook;

    [Fact]
    public void TestGetTitle()
    {
        string testEpubFilePath = Path.Combine(resourcesDirectory, "frankenstien.epub");
        eBook = _epubReaderService.ParsedEpubFile(testEpubFilePath);

        const string expectedTitle = "Frankenstein; Or, The Modern Prometheus";

        _epubReaderService.GetTitle(eBook)?.Should().Be(expectedTitle);
    }

    [Fact]
    public void TestGetAuthors()
    {
        string testEpubFilePath = Path.Combine(resourcesDirectory, "frankenstien.epub");
        eBook = _epubReaderService.ParsedEpubFile(testEpubFilePath);

        const string expectedAuthors = "Mary Wollstonecraft Shelley";

        _epubReaderService.GetAuthors(eBook)?.Should().Be(expectedAuthors);
    }

    [Fact]
    public void TestTableOfContents()
    {
        string testEpubFilePath = Path.Combine(resourcesDirectory, "frankenstien.epub");
        eBook = _epubReaderService.ParsedEpubFile(testEpubFilePath);

        string expectedTableOfContentsFilePath = Path.Combine(resourcesDirectory, "ExpectedTableOfContent.txt");
        string[] expectedTableOfContentsOutputFile = File.ReadAllLines(expectedTableOfContentsFilePath);

        string expectedTableOfContents = string.Join("\n", expectedTableOfContentsOutputFile);

        // _epubReaderService.GetTableOfContents(eBook)?.Should().Be(expectedTableOfContents);
    }

    [Fact]
    public void TestGetHtmlPage()
    {
        string testEpubFilePath = Path.Combine(resourcesDirectory, "frankenstien.epub");
        eBook = _epubReaderService.ParsedEpubFile(testEpubFilePath);

        string letter2PageFilePath = Path.Combine(resourcesDirectory, "ExpectedLetter2Page.txt");
        string chap1PageFilePath = Path.Combine(resourcesDirectory, "ExpectedChap01Page.txt");
        string chap21PageFilePath = Path.Combine(resourcesDirectory, "ExpectedChap21Page.txt");

        string[] letter2PageRawHtml = File.ReadAllLines(letter2PageFilePath);
        string[] chap1PageRawHtml = File.ReadAllLines(chap1PageFilePath);
        string[] chap21PageRawHtml = File.ReadAllLines(chap21PageFilePath);


        string expectedLetter2Page = string.Join("\n", letter2PageRawHtml);
        string expectedChap1Page = string.Join("\n", chap1PageRawHtml);
        string expectedChap21Page = string.Join("\n", chap21PageRawHtml);

        _epubReaderService.GetHtmlPage(eBook, "letter2").Should().Contain("id=\"letter2\"");
        _epubReaderService.GetHtmlPage(eBook, "letter2").Should().Be(expectedLetter2Page);
        _epubReaderService.GetHtmlPage(eBook, "chap01").Should().Contain("id=\"chap01\"");
        _epubReaderService.GetHtmlPage(eBook, "chap01").Should().Be(expectedChap1Page);
        _epubReaderService.GetHtmlPage(eBook, "chap21").Should().Contain("id=\"chap21\"");
        _epubReaderService.GetHtmlPage(eBook, "chap21").Should().Be(expectedChap21Page);
    }
}