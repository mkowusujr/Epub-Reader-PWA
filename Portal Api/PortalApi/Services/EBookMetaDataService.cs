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
            Path.Combine(Directory.GetCurrentDirectory(), @"Epub_Storage\\");
        string filePath = Path.Combine(epubStorageDirectory, eBookMetaFileData.FileName);

        EBookMetaData response = _context.EBookMetaData.Add(new EBookMetaData(filePath)).Entity;
        _context.SaveChanges();

        eBookMetaFileData.CopyTo(new FileStream(filePath, FileMode.Create));
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
