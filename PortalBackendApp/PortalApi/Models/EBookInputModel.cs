namespace PortalApi.Models;

public class EBookInputModel
{
    public int UserId { get; set; }
    public IFormFile EpubFile { get; set; }
}
