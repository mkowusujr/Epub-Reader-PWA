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
    public Annotation AddAnnotation(Annotation annotation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<Annotation> GetAnnotationForEBook(int ebookId, int userId);

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
    public bool DeleteAnnotationForEBook(int ebookId, int userId, int annotationId);
}
