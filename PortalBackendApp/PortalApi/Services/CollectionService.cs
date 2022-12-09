using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class CollectionService : ICollectionService
{
    private readonly PortalDbContext _context;

    public CollectionService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public Collection AddCollection(Collection collection)
    {
        try
        {
            Collection newCollection = _context.Collections
                .Add(new Collection { UserId = collection.UserId, Name = collection.Name })
                .Entity;
            _context.SaveChanges();
            return newCollection;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public bool AddEBookToCollectionForUser(int userId, int ebookId, int collectionId)
    {
        try
        {
            Collection fetchedCollection = _context.Collections.First(c => c.UserId == userId);
            EBook? fetchedEBook = _context.EBooks.First(e => e.UserId == userId);

            fetchedCollection.EBooks.Add(fetchedEBook);
            fetchedEBook.Collections.Add(fetchedCollection);

            _context.SaveChanges();
            return true;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public Collection GetCollectionForUser(int userId, int collectionId)
    {
        try
        {
            Collection fetchedCollection = _context.Collections.First(
                c => c.UserId == userId && c.CollectionId == collectionId
            );
            return fetchedCollection;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<Collection> GetCollectionsForUser(int userId)
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
    public bool RemoveEBookFromCollection(int userId, int ebookId, int collectionId)
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
                _context.SaveChanges();
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
