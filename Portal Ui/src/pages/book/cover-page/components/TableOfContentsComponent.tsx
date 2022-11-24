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
    <div>
      {tableOfContents?.map(chapter => (
        <div>
          <Link to={`chapter-id/${chapter.anchor}`}>
            <li>{chapter.title}</li>
          </Link>
        </div>
      ))}
    </div>
  );
}
