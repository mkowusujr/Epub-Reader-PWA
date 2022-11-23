using Portal_Api.Models;

namespace Portal_Api.Services;
public class EBookMetaDataService : IEBookMetaDataService
{
    private readonly EBookMetaDataDbContext _context;

    public EBookMetaDataService(EBookMetaDataDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public EBookMetaData AddBookMetaData(IFormFile eBookMetaFileData)
    {
        string epubStorageDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), @"EpubStorage\EpubFiles");
        string filePath = Path.Combine(epubStorageDirectory, eBookMetaFileData.FileName);

        // eBookMetaFileData.CopyTo(new FileStream(filePath, FileMode.Create));
        using (var stream = File.Create(filePath))
        {
            eBookMetaFileData.CopyTo(stream);
        }

        EBookMetaData response = _context.EBookMetaData.Add(new EBookMetaData(filePath)).Entity;
        _context.SaveChanges();

        return response;
    }

    /// <inheritdoc/>
    public bool DeleteEBookMetaData(int bookId)
    {
        var eBookMetaData = _context.EBookMetaData.FirstOrDefault(e => e.Id == bookId);
        if (eBookMetaData != null)
        {
            _context.Remove(eBookMetaData);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public EBookMetaData? GetEBookMetaData(int bookId)
    {
        return _context.EBookMetaData.FirstOrDefault(e => e.Id == bookId);
    }

    /// <inheritdoc/>
    public List<EBookMetaData> GetEBookMetaDataList()
    {
        return _context.EBookMetaData.ToList();
    }
}
