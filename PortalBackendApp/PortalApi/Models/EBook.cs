using System.Diagnostics.CodeAnalysis;
using EpubSharp;
using Newtonsoft.Json;
using PortalApi.Services;

namespace PortalApi.Models;

public class EBook
{
    public int EBookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public byte[] CoverImage { get; set; }
    public string TableOfContentsAsJson { get; set; }

    #region Relationships

    public int UserId { get; set; }
    public User User { get; set; }

    public List<Annotation>? Annotations { get; set; }

    public List<Collection> Collections { get; set; }

    #endregion

    public EBook(Stream epubFileStream, int userId, List<Collection> collections)
    {
        EpubReaderService epubReaderService = new EpubReaderService();
        EpubBook parsedEBook = epubReaderService.ParsedEpubFile(epubFileStream);

        Title = epubReaderService.GetTitle(parsedEBook);
        Author = epubReaderService.GetAuthors(parsedEBook);
        CoverImage = epubReaderService.GetCoverImage(parsedEBook);
        TableOfContentsAsJson = JsonConvert.SerializeObject(
            epubReaderService.GetTableOfContents(parsedEBook)
        );

        UserId = userId;
        Annotations = new List<Annotation>();
        Collections = collections;
    }

    [ExcludeFromCodeCoverage]
    public EBook()
    {
        // Empty constructor used for ef core
    }
}
