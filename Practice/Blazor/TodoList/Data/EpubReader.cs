namespace TodoList.Data;
using EpubSharp;
using AngleSharp;
using AngleSharp.Html.Parser;

public class EBook {
    public EpubBook book { get; } = EpubReader.Read(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Practice\Blazor\TodoList\Data\frankenstien.epub");
    public string hi = "Hello Ig";

    public async Task<string> GetHtml() {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);
        using var doc = await context.OpenAsync(req => req.Content(book.Resources.Html.ElementAt(1).TextContent));

        return doc.Body.InnerHtml.Trim();// var parser = new Parser();
        // var document = parser.Parse(book.Resources.Html);
        // string html = document.All.Where(m => m.LocalName == "body").First();
        // return html;
    }
}
