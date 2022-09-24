namespace TodoList.Data;
using EpubSharp;


public class EBook {
    public EpubBook book { get; } = EpubReader.Read(@"C:\Users\mokay\Local-Repositories\Epub-Reader-PWA\Practice\Blazor\TodoList\Data\frankenstien.epub");
    public string hi = "Hello Ig"; 
}
