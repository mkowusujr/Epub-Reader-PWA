using Portal_Api.Models;
namespace Portal_Api.Services;

public interface IEBookMetaDataService
{
    /// <summary>
    /// Adds the book meta data.
    /// </summary>
    /// <param name="eBookMetaData">The e book meta data.</param>
    /// <returns></returns>
    public EBookMetaData AddBookMetaData(EBookMetaData eBookMetaData);

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
