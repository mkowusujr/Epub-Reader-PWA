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
    public EBook AddEBookForUser(EBookInputModel ebookInputModel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    public EBook? GetEBookForUser(int userId, int eBookId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<EBook> GetEBooksForUser(int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="eBookId"></param>
    /// <returns></returns>
    public bool DeleteEBookForUser(int userId, int eBookId);
}
