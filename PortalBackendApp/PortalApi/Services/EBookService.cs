using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class EBookService : IEBookService
{
    private readonly PortalDbContext _context;

    /// <inheritdoc/>
    public EBookService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public EBook AddEBookForUser(EBookInputModel ebookInputModel)
    {
        try
        {
            EBook createdEBook = _context.EBooks
                .Add(new EBook(ebookInputModel.EpubFile, ebookInputModel.UserId))
                .Entity;
            _context.SaveChanges();
            return createdEBook;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public bool DeleteEBookForUser(int userId, int eBookId)
    {
        try
        {
            EBook fetchedEBook = _context.EBooks.First(e => e.EBookId == eBookId);
            _context.Remove(fetchedEBook);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<EBook> GetEBooksForUser(int userId)
    {
        try
        {
            return _context.EBooks.ToList();
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public EBook? GetEBookForUser(int userId, int eBookId)
    {
        try
        {
            return _context.EBooks.First(e => e.UserId == userId && e.EBookId == eBookId);
        }
        catch
        {
            throw new Exception();
        }
    }
}
