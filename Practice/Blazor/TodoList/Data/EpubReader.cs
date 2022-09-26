namespace TodoList.Data;
using EpubSharp;
using AngleSharp;
using AngleSharp.Html.Dom;
public class EBook {
    public EpubBook book { get; } = EpubReader.Read(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Practice\Blazor\TodoList\Data\frankenstien.epub");
    public string hi = "Hello Ig";

    public async Task<string> GetHtml() {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);
        using var doc = await context.OpenAsync(req => req.Content(book.Resources.Html.ElementAt(1).TextContent));

        return doc?.Body.InnerHtml.Trim();// var parser = new Parser();
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
}