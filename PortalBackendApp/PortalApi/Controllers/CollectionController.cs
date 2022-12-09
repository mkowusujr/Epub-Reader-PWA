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
    public ActionResult<Collection> AddCollection(Collection collection)
    {
        try
        {
            return Ok(_collectionService.AddCollection(collection));
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
    [HttpGet("users/user/{userId}/collections/collection/{collectionId}")]
    public ActionResult<Collection> GetCollectionForUser(int userId, int collectionId)
    {
        try
        {
            return Ok(_collectionService.GetCollectionForUser(userId, collectionId));
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
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpGet("users/user/{userId}/collections")]
    public ActionResult<List<EBook>> GetEBooksInCollectionForUser(int userId, int collectionId)
    {
        try
        {
            return Ok(_collectionService.GetEBooksInCollectionForUser(userId, collectionId));
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("users/user/{userId}/collections/collection/{collectionId}/add-ebook/{ebookId}")]
    public ActionResult<bool> AddEBookToCollectionForUser(int userId, int ebookId, int collectionId)
    {
        try
        {
            return Ok(
                _collectionService.AddEBookToCollectionForUser(userId, ebookId, collectionId)
            );
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("users/user/{userId}/collections/collection/{collectionId}/remove-ebook/{ebookId}")]
    public ActionResult<bool> RemoveEBookFromCollection(int userId, int ebookId, int collectionId)
    {
        try
        {
            return Ok(_collectionService.RemoveEBookFromCollection(userId, ebookId, collectionId));
        }
        catch
        {
            return BadRequest();
        }
    }
}
