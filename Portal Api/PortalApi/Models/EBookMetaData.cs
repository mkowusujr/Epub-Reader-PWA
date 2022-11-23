using System.ComponentModel.DataAnnotations;

namespace Portal_Api.Models;

public class EBookMetaData
{
    [Key]
    public int Id { get; set; }

    public string FilePath { get; set; }

    public bool IsMarkAsFavorite { get; set; }

    public EBookMetaData(string filePath) => FilePath = filePath;
}
