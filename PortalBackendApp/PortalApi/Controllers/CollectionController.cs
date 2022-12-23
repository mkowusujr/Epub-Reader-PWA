using Microsoft.AspNetCore.Mvc;
using PortalApi.Services.Interfaces;
using PortalApi.Models;
using Microsoft.AspNetCore.Authorization;

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
    [HttpPost, Authorize]
    public async Task<ActionResult<Collection>> AddCollectionAsync(Collection collection)
    {
        try
        {
            Collection createdCollection = await _collectionService.AddCollectionAsync(collection);
            return CreatedAtAction(nameof(GetCollectionForUserAsync), new { }, createdCollection);
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
    public async Task<ActionResult<Collection>> GetCollectionsForUserAsync()
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(await _collectionService.GetCollectionsForUserAsync(userId));
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
    [HttpGet("collection/{collectionId}"), Authorize]
    public async Task<ActionResult<Collection>> GetCollectionForUserAsync(int collectionId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
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
    [HttpGet("collection/{collectionId}/ebooks"), Authorize]
    public async Task<ActionResult<List<EBook>>> GetEBooksInCollectionForUserAsync(int collectionId)
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(
                await _collectionService.GetEBooksInCollectionForUserAsync(userId, collectionId)
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
    /// <param name="ebookId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpPut("collection/{collectionId}/add-ebook/{ebookId}"), Authorize]
    public async Task<ActionResult<bool>> AddEBookToCollectionForUserAsync(
        int ebookId,
        int collectionId
    )
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(
                await _collectionService.AddEBookToCollectionForUserAsync(
                    userId,
                    ebookId,
                    collectionId
                )
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
    /// <param name="ebookId"></param>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpPut("collection/{collectionId}/remove-ebook/{ebookId}"), Authorize]
    public async Task<ActionResult<bool>> RemoveEBookFromCollectionAsync(
        int ebookId,
        int collectionId
    )
    {
        try
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            return Ok(
                await _collectionService.RemoveEBookFromCollectionAsync(
                    userId,
                    ebookId,
                    collectionId
                )
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
