using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class EBookService : IEBookService
{
    private readonly PortalDbContext _context;

    /// <inheritdoc/>
    public EBookService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public async Task<EBook> AddEBookForUserAsync(EBookInputModel ebookInputModel)
    {
        try
        {
            EBook createdEBook = _context.EBooks
                .Add(
                    new EBook(
                        epubFileStream: ebookInputModel.EpubFile.OpenReadStream(),
                        userId: ebookInputModel.UserId,
                        collections: ebookInputModel.Collections
                    )
                )
                .Entity;
            await _context.SaveChangesAsync();
            return createdEBook;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteEBookForUserAsync(int userId, int eBookId)
    {
        try
        {
            EBook fetchedEBook = _context.EBooks.First(e => e.EBookId == eBookId);
            _context.Remove(fetchedEBook);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public List<EBook> GetEBooksForUser(int userId)
    {
        try
        {
            return _context.EBooks.ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<EBook?> GetEBookForUserAsync(int userId, int eBookId)
    {
        try
        {
            EBook? fetchedEBook = await _context.EBooks.FindAsync(eBookId);
            if (fetchedEBook == null)
            {
                throw new Exception($"EBook {eBookId} was not found");
            }
            else if (fetchedEBook.UserId != userId)
            {
                throw new Exception($"User {userId} doesn't own ebook {eBookId}");
            }
            return fetchedEBook;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
