using PortalApi.Models;

namespace PortalApi.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface ICollectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    public Collection AddCollection(Collection collection);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    public Collection GetCollectionForUser(int userId, int collectionId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<Collection> GetCollectionsForUser(int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    public List<EBook> GetEBooksInCollectionForUser(int userId, int collectionId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ebookId"></param>
    /// <param name="collectionId"></param>
    public bool AddEBookToCollectionForUser(int userId, int ebookId, int collectionId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    public bool RemoveEBookFromCollection(int userId, int ebookId, int collectionId);
}
