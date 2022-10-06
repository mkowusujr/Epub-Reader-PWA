namespace EReaderSharp.Tests.Data.Tests;
using EReaderSharp.Data;

public class UnitTest1
{
    const string TestEpubFile = @"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Practice\Blazor\TodoList\Data\frankenstien.epub";

    [Fact]
    public void TestGetTitle()
    {
        EpubReaderService.OpenEBook(TestEpubFile);
        string expectedTitle = "Frankenstein; Or, The Modern Prometheus";
        Assert.Equal(expectedTitle, EpubReaderService.GetTitle());
    }

    [Fact]
    public void TestGetAuthors()
    {
        EpubReaderService.OpenEBook(TestEpubFile);
        string expectedAuthors = "Mary Wollstonecraft Shelley";
        Assert.Equal(expectedAuthors, EpubReaderService.GetAuthors());
    }

    [Fact]
    public void TestTableOfContents()
    {
        EpubReaderService.OpenEBook(TestEpubFile);
        
        
        string[] ExpectedTableOfContentsOutputFile = System.IO.File.ReadAllLines(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Epub Reader Sharp\EReaderSharp\EReaderSharp.Tests\Data.Tests\ExpectedTableOfContent.txt");
        string expectedTableOfContents = String.Join("\n", ExpectedTableOfContentsOutputFile);
        Assert.Equal(expectedTableOfContents, EpubReaderService.GetTableOfContents());
    }

    [Fact]
    public void TestGetHtmlPage()
    {
        EpubReaderService.OpenEBook(TestEpubFile);

        string[] expectedLetter2Page = System.IO.File.ReadAllLines(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Epub Reader Sharp\EReaderSharp\EReaderSharp.Tests\Data.Tests\ExpectedLetter2Page.txt");
        string? actualLetter2Page = EpubReaderService.GetHtmlPage("letter2");
        Assert.Equal(String.Join("\n", expectedLetter2Page), actualLetter2Page);

        string[] expectedChap1Page = System.IO.File.ReadAllLines(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Epub Reader Sharp\EReaderSharp\EReaderSharp.Tests\Data.Tests\ExpectedChap1Page.txt");
        string? actualChap1Page = EpubReaderService.GetHtmlPage("chap01");
        Assert.Equal(String.Join("\n", expectedChap1Page), actualChap1Page);

        string[] expectedChap21Page = System.IO.File.ReadAllLines(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Epub Reader Sharp\EReaderSharp\EReaderSharp.Tests\Data.Tests\ExpectedChap21Page.txt");
        string? actualChap21Page = EpubReaderService.GetHtmlPage("chap21");
        Assert.Equal(String.Join("\n", expectedChap21Page), actualChap21Page);
    }
}