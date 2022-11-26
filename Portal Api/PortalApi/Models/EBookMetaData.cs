﻿using System.ComponentModel.DataAnnotations;
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

    public string FileName { get; set; }

    public bool IsMarkAsFavorite { get; set; }

    public EBookMetaData(string fileName) {
        FileName = fileName;

        EpubReaderService epubReaderService = new EpubReaderService();
        EpubBook parsedEBook = epubReaderService.ParsedEpubFile(FileName);

        Title = epubReaderService.GetTitle(parsedEBook);
        Author = epubReaderService.GetAuthors(parsedEBook);
        CoverImage = epubReaderService.GetCoverImage(parsedEBook);
        IsMarkAsFavorite = false;
    }
}
