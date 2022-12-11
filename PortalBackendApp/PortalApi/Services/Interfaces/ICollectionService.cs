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
    public Task<Collection> AddCollectionAsync(Collection collection);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    public Task<Collection> GetCollectionForUserAsync(int userId, int collectionId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<Collection>? GetCollectionsForUser(int userId);

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
    public Task<bool> AddEBookToCollectionForUserAsync(int userId, int ebookId, int collectionId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    public Task<bool> RemoveEBookFromCollectionAsync(int userId, int ebookId, int collectionId);
}
