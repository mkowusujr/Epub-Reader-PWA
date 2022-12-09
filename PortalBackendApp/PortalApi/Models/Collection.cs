namespace PortalApi.Models;

public class Collection
{
    public int CollectionId { get; set; }

    public string? Name { get; set; }

    #region Relationships

    public int UserId { get; set; }
    public User User { get; set; }

    public List<EBook> EBooks { get; set; }

    #endregion
}
