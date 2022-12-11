namespace PortalApi.Models;

public class Annotation
{
    public int AnnotationId { get; set; }
    public string Comment { get; set; }

    #region Relationships

    public int UserId { get; set; }
    public User User { get; set; }

    public int EBookId { get; set; }
    public EBook EBook { get; set; }

    #endregion

    public Annotation(string comment, int userId, int eBookId) =>
        (Comment, UserId, EBookId) = (comment, userId, eBookId);

    public Annotation()
    { 
        // Empty constructor used for ef core
    }
}
