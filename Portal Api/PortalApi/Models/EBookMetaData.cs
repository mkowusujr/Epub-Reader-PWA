using System.ComponentModel.DataAnnotations;
using EpubSharp;
using Portal_Api.Services;
namespace Portal_Api.Models;

public class EBookMetaData
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public byte[] CoverImage { get; set; }

    public string FilePath { get; set; }

    public bool IsMarkAsFavorite { get; set; }

    public EBookMetaData(string filePath) {
        FilePath = filePath;

        EpubReaderService epubReaderService = new EpubReaderService();
        EpubBook parsedEBook = epubReaderService.ParsedEpubFile(FilePath);

        Title = epubReaderService.GetTitle(parsedEBook);
        Author = epubReaderService.GetAuthors(parsedEBook);
        CoverImage = epubReaderService.GetCoverImage(parsedEBook);
        IsMarkAsFavorite = false;
    }
}
