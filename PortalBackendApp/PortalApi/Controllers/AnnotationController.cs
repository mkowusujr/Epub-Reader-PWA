using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;
using Microsoft.AspNetCore.Authorization;

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
    [HttpPost, Authorize]
    public async Task<ActionResult<Annotation>> AddAnnotationAsync(Annotation annotation)
    {
        try
        {
            Annotation createdAnnotation = await _annotationService.AddAnnotationAsync(annotation);
            return CreatedAtAction(
                nameof(GetAnnotationForEBook),
                new
                {
                    eBookId = createdAnnotation.EBookId
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
    [HttpGet("ebooks/ebook/{ebookId}"), Authorize]
    public ActionResult<Annotation> GetAnnotationForEBook(int ebookId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
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
    [HttpGet, Authorize]
    public ActionResult<List<Annotation>> GetAnnotationsForUser()
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
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
    [HttpDelete("ebooks/ebook/{ebookId}/annotations/annotation/{annotationId}"), Authorize]
    public ActionResult<bool> DeleteAnnotationForEBookAsync(int ebookId, int annotationId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(
                _annotationService.DeleteAnnotationForEBookAsync(ebookId, userId, annotationId)
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
    /// <param name="annotationId"></param>
    /// <returns></returns>
    [HttpGet("ebooks/ebook/{ebookId}/annotations/annotation/{annotationId}"), Authorize]
    public async Task<ActionResult<Annotation>> GetAnnotationAsync(int ebookId, int annotationId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
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
