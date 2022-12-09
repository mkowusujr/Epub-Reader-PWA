using PortalApi.Models;
using PortalApi.Services.Interfaces;

namespace PortalApi.Services;

public class AnnotationService : IAnnotationService
{
    private readonly PortalDbContext _context;

    public AnnotationService(PortalDbContext context) => _context = context;

    /// <inheritdoc/>
    public Annotation AddAnnotation(Annotation annotation)
    {
        try
        {
            Annotation createdAnnotation = _context.Annotations
                .Add(
                    new Annotation
                    {
                        UserId = annotation.UserId,
                        EBookId = annotation.EBookId,
                        Comment = annotation.Comment
                    }
                )
                .Entity;
            _context.SaveChanges();
            return createdAnnotation;
        }
        catch
        {
            throw new Exception();
        }
    }

    /// <inheritdoc/>
    public List<Annotation> GetAnnotationForEBook(int ebookId, int userId)
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
    public bool DeleteAnnotationForEBook(int ebookId, int userId, int annotationId)
    {
        try {
            Annotation fetchedAnnotation = _context.Annotations.First(a => a.UserId == userId && a.EBookId == ebookId && a.AnnotationId == annotationId);
            _context.Remove(fetchedAnnotation);
            _context.SaveChanges();
            return true;
        }
        catch {
            throw new Exception();
        }
    }
}
