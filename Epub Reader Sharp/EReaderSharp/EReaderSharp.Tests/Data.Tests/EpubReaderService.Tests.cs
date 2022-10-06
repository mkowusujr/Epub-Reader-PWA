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
    public async void TestTableOfContents()
    {
        EpubReaderService.OpenEBook(TestEpubFile);
        
        
        string[] ExpectedTableOfContentsOutputFile = System.IO.File.ReadAllLines(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Epub Reader Sharp\EReaderSharp\EReaderSharp.Tests\Data.Tests\ExpectedTableOfContent.txt");
        string expectedTableOfContents = String.Join("\n", ExpectedTableOfContentsOutputFile);
        Assert.Equal(expectedTableOfContents, await EpubReaderService.GetTableOfContents());
    }
}