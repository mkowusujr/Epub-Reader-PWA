## Install Package WIth
`dotnet add package EpubSharp.dll`

## Reading an EPUB
```
// Read an epub file
EpubBook book = EpubReader.Read("my.epub");

// Read metadata
string title = book.Title;
string[] authors = book.Authors;
Image cover = book.CoverImage;

// Get table of contents
ICollection<EpubChapter> chapters = book.TableOfContents;

// Get contained files
ICollection<EpubTextFile> html = book.Resources.Html;
ICollection<EpubTextFile> css = book.Resources.Css;
ICollection<EpubByteFile> images = book.Resources.Images;
ICollection<EpubByteFile> fonts = book.Resources.Fonts;

// Convert to plain text
string text = book.ToPlainText();

// Access internal EPUB format specific data structures.
EpubFormat format = book.Format;
OcfDocument ocf = format.Ocf;
OpfDocument opf = format.Opf;
NcxDocument ncx = format.Ncx;
NavDocument nav = format.Nav;

// Create an EPUB
EpubWriter.Write(book, "new.epub");
```