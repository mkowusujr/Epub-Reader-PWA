using EpubSharp;

EpubBook book = EpubReader.Read("frankenstien.epub");

Console.WriteLine(book.Title);
Console.WriteLine();

foreach(string author in book.Authors) {
    Console.WriteLine(author);
} 
Console.WriteLine();

Console.WriteLine(book.TableOfContents.Count());
foreach(EpubChapter chapter in book.TableOfContents) {
    Console.WriteLine(chapter.Title);
} 
Console.WriteLine();

ICollection<EpubTextFile> htmlFiles = book.Resources.Html;
Console.WriteLine(htmlFiles.Count());