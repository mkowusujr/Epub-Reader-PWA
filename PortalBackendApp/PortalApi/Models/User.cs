namespace PortalApi.Models;

public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    #region Relationships

    public List<EBook> EBook { get; set; }

    public List<Collection> Collections { get; set; }

    public List<Annotation> Annotations { get; set; }

    #endregion
}
