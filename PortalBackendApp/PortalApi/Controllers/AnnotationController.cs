using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;

namespace PortalApi.Controllers;

[ApiController]
[Route("api/annotations")]
public class AnnotationController : ControllerBase
{
    private readonly IAnnotationService _annotationService;

    /// <summary>
    ///
    /// /// </summary>
    /// <param name="annotationService"></param>
    public AnnotationController(IAnnotationService annotationService) =>
        _annotationService = annotationService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="annotation"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<Annotation> AddAnnotation(Annotation annotation)
    {
        try
        {
            return Ok(_annotationService.AddAnnotation(annotation));
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/ebooks/ebook/{ebookId}")]
    public ActionResult<Annotation> GetAnnotationForEBook(int ebookId, int userId)
    {
        try
        {
            return Ok(_annotationService.GetAnnotationForEBook(ebookId, userId));
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}")]
    public ActionResult<List<Annotation>> GetAnnotationsForUser(int userId)
    {
        try
        {
            return Ok(_annotationService.GetAnnotationsForUser(userId));
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("users/user/{userId}/ebooks/ebook/{ebookId}/annotations/annotation/{annotationId}")]
    public ActionResult<bool> DeleteAnnotationForEBook(int ebookId, int userId, int annotationId)
    {
        try
        {
            return Ok(_annotationService.DeleteAnnotationForEBook(ebookId, userId, annotationId));
        }
        catch
        {
            return BadRequest();
        }
    }
}
