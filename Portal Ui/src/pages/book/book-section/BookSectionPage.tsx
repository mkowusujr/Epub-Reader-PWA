// https://thecodemon.com/react-on-page-scroll-progress-bar-with-web-api-example/#:~:text=The%20progressBar%20%28%29%20function%20handles%20the%20progress%20of,of%20pixels%20that%20an%20element%20is%20vertically%20scrolled.
import DOMPurify from 'isomorphic-dompurify';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import {
  getPage,
  getTableOfContents
} from '../../../api/EpubReaderApiService';
import { IChapterMetaData } from '../../../models/IChapterMetaData';
import './BookSection.scss';

export function BookSectionPage() {
  const [bookSectionHtml, setBookSectionHtml] = useState<string | undefined>(
    undefined
  );
  const { ebookId, chapterId } = useParams();

  const fetchSectionDataFromApi = (ebookId: number, chapterId: string) => {
    let selectedChapter: string | undefined;

    getTableOfContents(ebookId)
      .then((chapters: IChapterMetaData[]) => {
        selectedChapter = chapters.find(c => c.anchor == chapterId)?.fileName;
        return selectedChapter;
      })
      .then(selectedChapter => {
        if (selectedChapter) {
          getPage(ebookId, selectedChapter).then(pageContent => {
            setBookSectionHtml(pageContent);
          });
        }
      })
      .catch(error => console.error(error));
  };

  useEffect(() => {
    if (ebookId && chapterId) {
      fetchSectionDataFromApi(+ebookId, chapterId);
    }
  }, []);

  return (
    <div className='book-section-page'>
      {bookSectionHtml ? (
        <div className='page-content'
          dangerouslySetInnerHTML={{
            __html: DOMPurify.sanitize(bookSectionHtml)
          }}
        />
      ) : (
        <>
          <p>Page Loading</p>
        </>
      )}
    </div>
  );
}
