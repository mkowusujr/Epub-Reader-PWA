namespace PortalApi.Services;
using AngleSharp.Html.Dom;

/// <summary>
/// Extention methods for the epub reader service
/// </summary>
public static class EpubReaderServiceExtentions
{
    /// <summary>
    /// Updates the dom elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="updateMethod"></param>
    /// <returns></returns>
    public static IEnumerable<T> UpdateDomElements<T>(this IEnumerable<T> items, Action<T> updateMethod)
    {
        foreach (T item in items)
        {
            updateMethod(item);
        }
        return items;
    }

    /// <summary>
    /// Checks to see if the current section has the book section name in its first anchor element's id
    /// </summary>
    /// <param name="dom">The Html Dom</param>
    /// <param name="bookSection">The section of the epub to fetch, usually are chapters</param>
    /// <returns>Whether or not the id was found</returns>
    public static bool IsCorrectPage(this AngleSharp.Dom.IDocument dom, string bookSection)
    {
        IHtmlAnchorElement? foundAnchorElement = dom.QuerySelectorAll("a")
            .OfType<IHtmlAnchorElement>()
            .FirstOrDefault(a => a.Id == bookSection);

        return foundAnchorElement != null;
    }
}
