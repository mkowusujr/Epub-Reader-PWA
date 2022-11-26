using Portal_Api.Models;

namespace Portal_Api.Services;
public class EBookMetaDataService : IEBookMetaDataService
{
    private readonly EBookMetaDataDbContext _context;
    private readonly string EpubStorageDirectory;

    public EBookMetaDataService(EBookMetaDataDbContext context)
    {
        _context = context;
        EpubStorageDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"EpubStorage\EpubFiles");
    }

    /// <inheritdoc/>
    public EBookMetaData AddBookMetaData(IFormFile eBookMetaFileData)
    {
        string filePath = Path.Combine(EpubStorageDirectory, eBookMetaFileData.FileName);

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
            DeleteEpubFile(eBookMetaData.FilePath);
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

    /// <summary>Deletes a epub file from local storage</summary>
    /// <param name="fileName">The file path of the file being deleted</param>
    private void DeleteEpubFile(string fileName){
        if (File.Exists(fileName))
        {
            try {
                File.Delete(fileName);
            }
            catch (Exception e) {
                Console.WriteLine("The deletion failed: {0}", e.Message);
            }
        }
        else {
            Console.WriteLine("Specified file doesn't exist");
        }
    }
}
