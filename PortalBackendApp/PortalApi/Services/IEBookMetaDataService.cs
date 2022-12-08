using PortalApi.Models;

namespace PortalApi.Services;
public interface IEBookMetaDataService
{
    /// <summary>
    /// Adds the book meta data.
    /// </summary>
    /// <param name="eBookMetaFileData">The e book meta file data.</param>
    /// <returns></returns>
    public EBookMetaData AddBookMetaData(IFormFile eBookMetaFileData);

    /// <summary>
    /// Gets the e book meta data.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    public EBookMetaData? GetEBookMetaData(int bookId);

    /// <summary>
    /// Gets the e book meta data list.
    /// </summary>
    /// <returns></returns>
    public List<EBookMetaData> GetEBookMetaDataList();

    /// <summary>
    /// Deletes the e book meta data.
    /// </summary>
    /// <param name="bookId">The book identifier.</param>
    /// <returns></returns>
    public bool DeleteEBookMetaData(int bookId);
}
