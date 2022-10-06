namespace TodoList.Data;
using EpubSharp;
using AngleSharp;
using AngleSharp.Html.Dom;
public class EBook {
    public EpubBook book { get; } = EpubReader.Read(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Practice\Blazor\TodoList\Data\frankenstien.epub");
    public string hi = "Hello Ig";

    public async Task<string> GetHtml() {
        // var config = Configuration.Default;
        // using var context = BrowsingContext.New(config);
        // using var doc = await context.OpenAsync(req => req.Content(book.Resources.Html.ElementAt(1).TextContent));
        return await book.Resources.Html.ToParsedHtml().Where(d => d.All.Where(m => m.LocalName == "body").First());
        // return doc?.Body.InnerHtml.Trim();// var parser = new Parser();
        // var document = parser.Parse(book.Resources.Html);
        // string html = document.All.Where(m => m.LocalName == "body").First();
        // return html;
    }

    public async Task GetTOF(){
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);
        using var doc = await context.OpenAsync(req => req.Content(book.Resources.Html.ElementAt(0).TextContent));
        var anchorElements = doc.QuerySelectorAll("a")
                            .OfType<IHtmlAnchorElement>()
                            .SetHref(a => a.Href = a.Href.Split('#').Last())
                            .ToList();
        // .Select(a => ((IHtmlAnchorElement)m).Href)
        // .ToList();
        // Console.WriteLine(anchorElement.Href);
        foreach (var a in anchorElements){
            Console.WriteLine(a.Href , ", ");
        }
        // var aes = doc.All.Where(m => ((IHtmlAnchorElement)m).Href).ToList();
        // Console.WriteLine(aes);
        // foreach (AngleSharp.Dom.IElement ae in aes)
        // {
        // }
    }
}

public static class MyExtentions{
    public static IEnumerable<T> SetHref<T>(this IEnumerable<T> items, Action<T> updateMethod){
        foreach (T item in items){
            updateMethod(item);
        }
        return items;
    }

    public static async Task<IEnumerable<T>> ToParsedHtml<T>(this IEnumerable<AngleSharp.Dom.IElement> items)
    {
            var config = Configuration.Default;
            using var context = BrowsingContext.New(config);
        IEnumerable<AngleSharp.Dom> parsedItems;
        foreach (AngleSharp.Dom item in items)
        {
            using var doc = await context.OpenAsync(req => req.Content(item.TextContent));
            parsedItems.Append(doc);
        }
        return parsedItems;
    }
}