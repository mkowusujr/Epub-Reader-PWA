namespace PortalApi.Services;

using System.Diagnostics.CodeAnalysis;
using AngleSharp.Html.Dom;

/// <summary>
/// Extention methods for the epub reader service
/// /// </summary>
[ExcludeFromCodeCoverage]
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
}
