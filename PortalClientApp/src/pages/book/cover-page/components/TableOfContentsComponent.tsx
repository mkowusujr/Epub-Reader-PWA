import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getTableOfContents } from '../../../../api/EpubReaderApiService';
import { IChapterMetaData } from '../../../../models/IChapterMetaData';

/** */
export function TableOfContentsComponent(props: any) {
  const [tableOfContents, setTableOfContents] = useState<
    IChapterMetaData[] | undefined
  >(undefined);

  const fetchTableOfContents = (ebookId: number) => {
    getTableOfContents(ebookId)
      .then(fetchedTableOfContents =>
        setTableOfContents(fetchedTableOfContents)
      )
      .catch(error => console.error(error));
  };

  useEffect(() => {
    fetchTableOfContents(props.eBook.id);
  }, []);

  return (
    <div className='table-of-contents'>
      <h1>Table of Contents</h1>
      {tableOfContents?.map((chapter) => (
        <div>
          <Link to={`chapter-id/${chapter.anchor}`}>
            <li key={chapter.anchor}>{chapter.title}</li>
          </Link>
        </div>
      ))}
    </div>
  );
}
