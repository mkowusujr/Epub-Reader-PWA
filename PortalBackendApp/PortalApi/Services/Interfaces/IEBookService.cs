using PortalApi.Models;

namespace PortalApi.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IEBookService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ebookInputModel"></param>
    /// <returns></returns>
    public Task<EBook> AddEBookForUserAsync(EBookInputModel ebookInputModel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    public Task<EBook?> GetEBookForUserAsync(int userId, int eBookId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<EBook>> GetEBooksForUserAsync(int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    public Task<bool> DeleteEBookForUserAsync(int userId, int eBookId);
}
