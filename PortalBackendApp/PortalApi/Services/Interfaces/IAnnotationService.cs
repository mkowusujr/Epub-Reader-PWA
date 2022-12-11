using PortalApi.Models;

namespace PortalApi.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IAnnotationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="annotation"></param>
    /// <returns></returns>
    public Task<Annotation> AddAnnotationAsync(Annotation annotation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<Annotation> GetAnnotationsForEBook(int ebookId, int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<Annotation> GetAnnotationsForUser(int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <param name="annotationId"></param>
    /// <returns></returns>
    public Task<bool> DeleteAnnotationForEBookAsync(int ebookId, int userId, int annotationId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <param name="annotationId"></param>
    /// <returns></returns>
    public Task<Annotation> GetAnnotationAsync(int ebookId, int userId, int annotationId);
}
