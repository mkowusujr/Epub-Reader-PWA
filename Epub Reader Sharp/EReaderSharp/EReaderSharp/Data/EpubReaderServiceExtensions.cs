namespace EReaderSharp.Data;
using EpubSharp;
using AngleSharp;

/// <summary>
/// 
/// </summary>
public static class EpubReaderServiceExtentions
{
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static async Task<IEnumerable<AngleSharp.Dom.Document>> ToParsedHtml<T>(this ICollection<EpubTextFile> items)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);

        List<AngleSharp.Dom.Document> parsedItems = new List<AngleSharp.Dom.Document>();

        foreach (EpubTextFile item in items)
        {
            using var parsedHtml = await context.OpenAsync(req => req.Content(item.TextContent));
            _ = parsedItems.Append(parsedHtml);
        }
        return (IEnumerable<AngleSharp.Dom.Document>)parsedItems;
    }
}
