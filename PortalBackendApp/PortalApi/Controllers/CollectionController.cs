using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;

namespace PortalApi.Controllers;

[ApiController]
[Route("api/collections")]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService _collectionService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="collectionService"></param>
    public CollectionController(ICollectionService collectionService) =>
        _collectionService = collectionService;

    /// <summary>
    ///
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Collection>> AddCollectionAsync(Collection collection)
    {
        try
        {
            Collection createdCollection = await _collectionService.AddCollectionAsync(collection);
            return CreatedAtAction(
                nameof(GetCollectionForUserAsync),
                new { userId = createdCollection.UserId, eBookId = createdCollection.CollectionId },
                createdCollection
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
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/collections/collection/{collectionId}")]
    public async Task<ActionResult<Collection>> GetCollectionForUserAsync(int userId, int collectionId)
    {
        try
        {
            return Ok(await _collectionService.GetCollectionForUserAsync(userId, collectionId));
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
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/collections")]
    public ActionResult<List<EBook>> GetEBooksInCollectionForUser(int userId, int collectionId)
    {
        try
        {
            return Ok(_collectionService.GetEBooksInCollectionForUser(userId, collectionId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("users/user/{userId}/collections/collection/{collectionId}/add-ebook/{ebookId}")]
    public async Task<ActionResult<bool>> AddEBookToCollectionForUserAsync(
        int userId,
        int ebookId,
        int collectionId
    )
    {
        try
        {
            return Ok(
                await _collectionService.AddEBookToCollectionForUserAsync(userId, ebookId, collectionId)
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("users/user/{userId}/collections/collection/{collectionId}/remove-ebook/{ebookId}")]
    public async Task<ActionResult<bool>> RemoveEBookFromCollectionAsync(
        int userId,
        int ebookId,
        int collectionId
    )
    {
        try
        {
            return Ok(
                await _collectionService.RemoveEBookFromCollectionAsync(userId, ebookId, collectionId)
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
