using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class AnnotationService : IAnnotationService
{
    private readonly PortalDbContext _context;

    public AnnotationService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public async Task<Annotation> AddAnnotationAsync(Annotation annotation)
    {
        try
        {
            Annotation createdAnnotation = _context.Annotations
                .Add(
                    new Annotation(
                        userId: annotation.UserId,
                        eBookId: annotation.EBookId,
                        comment: annotation.Comment
                    )
                )
                .Entity;
            await _context.SaveChangesAsync();
            return createdAnnotation;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<Annotation> GetAnnotationsForEBook(int ebookId, int userId)
    {
        try
        {
            return _context.Annotations.ToList();
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<Annotation> GetAnnotationsForUser(int userId)
    {
        try
        {
            return _context.Annotations.Where(a => a.UserId == userId).ToList();
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAnnotationForEBookAsync(int ebookId, int userId, int annotationId)
    {
        try
        {
            Annotation? fetchedAnnotation = await _context.Annotations.FindAsync(annotationId);

            if (fetchedAnnotation == null)
            {
                throw new Exception($"Annotation {annotationId} doesn't exist");
            }
            else if (fetchedAnnotation.UserId != userId || fetchedAnnotation.EBookId != ebookId)
            {
                throw new Exception(
                    $"Annotation either doesn't belong to User {userId}, EBook {ebookId} or both"
                );
            }

            _context.Remove(fetchedAnnotation);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public async Task<Annotation> GetAnnotationAsync(int ebookId, int userId, int annotationId)
    {
        try
        {
            Annotation? fetchedAnnotation = await _context.Annotations.FindAsync(annotationId);

            if (fetchedAnnotation == null)
            {
                throw new Exception($"Annotation {annotationId} doesn't exist");
            }
            else if (fetchedAnnotation.UserId != userId || fetchedAnnotation.EBookId != ebookId)
            {
                throw new Exception(
                    $"Annotation either doesn't belong to User {userId}, EBook {ebookId} or both"
                );
            }

            return fetchedAnnotation;
        }
        catch
        {
            throw new Exception();
        }
    }
}
