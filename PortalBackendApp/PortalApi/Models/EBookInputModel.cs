namespace PortalApi.Models;

public class EBookInputModel
{
    public int UserId { get; set; }
    public IFormFile EpubFile { get; set; }
    public List<Collection> Collections { get; set; }

    public EBookInputModel(int userId, IFormFile epubFile, List<Collection> collections) =>
        (UserId, EpubFile, Collections) = (userId, epubFile, collections);
}
