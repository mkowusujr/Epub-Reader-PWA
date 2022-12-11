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
    public async Task<ActionResult<Annotation>> AddAnnotationAsync(Annotation annotation)
    {
        try
        {
            Annotation createdAnnotation = await _annotationService.AddAnnotationAsync(annotation);
            return CreatedAtAction(
                nameof(GetAnnotationForEBook),
                new
                {
                    userId = createdAnnotation.UserId,
                    eBookId = createdAnnotation.EBookId,
                    annotationId = createdAnnotation.AnnotationId
                },
                createdAnnotation
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            return Ok(_annotationService.GetAnnotationsForEBook(ebookId, userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <param name="annotationId"></param>
    /// <returns></returns>
    [HttpDelete("users/user/{userId}/ebooks/ebook/{ebookId}/annotations/annotation/{annotationId}")]
    public ActionResult<bool> DeleteAnnotationForEBookAsync(int ebookId, int userId, int annotationId)
    {
        try
        {
            return Ok(_annotationService.DeleteAnnotationForEBookAsync(ebookId, userId, annotationId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="ebookId"></param>
    /// <param name="userId"></param>
    /// <param name="annotationId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/ebooks/ebook/{ebookId}/annotations/annotation/{annotationId}")]
    public async Task<ActionResult<Annotation>> GetAnnotationAsync(
        int ebookId,
        int userId,
        int annotationId
    )
    {
        try
        {
            Annotation? fetchedAnnotation = await _annotationService.GetAnnotationAsync(
                ebookId,
                userId,
                annotationId
            );
            return fetchedAnnotation != null ? fetchedAnnotation : NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
