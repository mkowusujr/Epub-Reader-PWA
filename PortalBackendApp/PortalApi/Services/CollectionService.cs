using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class CollectionService : ICollectionService
{
    private readonly PortalDbContext _context;

    public CollectionService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public async Task<Collection> AddCollectionAsync(Collection collection)
    {
        try
        {
            Collection newCollection = _context.Collections
                .Add(
                    new Collection(
                        userId: collection.UserId,
                        name: collection.Name,
                        eBooks: collection.EBooks
                    )
                )
                .Entity;
            await _context.SaveChangesAsync();
            return newCollection;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<bool> AddEBookToCollectionForUserAsync(int userId, int ebookId, int collectionId)
    {
        try
        {
            Collection fetchedCollection = _context.Collections.First(c => c.UserId == userId);
            EBook? fetchedEBook = _context.EBooks.First(e => e.UserId == userId);

            fetchedCollection.EBooks.Add(fetchedEBook);
            fetchedEBook.Collections.Add(fetchedCollection);

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<Collection> GetCollectionForUserAsync(int userId, int collectionId)
    {
        try
        {
            Collection? fetchedCollection = await _context.Collections.FindAsync(collectionId);

            if (fetchedCollection == null)
            {
                throw new Exception($"Collection {collectionId} doesn't exist");
            }
            else if (fetchedCollection.UserId != userId)
            {
                throw new Exception($"User {userId} doesn't own Collection {collectionId}");
            }

            return fetchedCollection;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<Collection>? GetCollectionsForUser(int userId)
    {
        try
        {
            return _context.Users.First(u => u.UserId == userId).Collections;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<EBook> GetEBooksInCollectionForUser(int userId, int collectionId)
    {
        try
        {
            List<EBook> collectionEBooks = _context.Collections
                .First(c => c.UserId == userId && c.CollectionId == collectionId)
                .EBooks;
            return collectionEBooks;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveEBookFromCollectionAsync(int userId, int ebookId, int collectionId)
    {
        try
        {
            Collection fetchedCollection = _context.Collections.First(c => c.UserId == userId);
            EBook? fetchedEBook = fetchedCollection?.EBooks?.First(e => e.UserId == userId);
            if (fetchedCollection != null && fetchedEBook != null)
            {
                fetchedCollection.EBooks = fetchedCollection.EBooks
                    .Where(e => e.EBookId != ebookId)
                    .ToList();
                fetchedEBook.Collections = fetchedEBook.Collections
                    .Where(c => c.CollectionId != collectionId)
                    .ToList();
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            throw new Exception();
        }
    }
}
