import DOMPurify from 'isomorphic-dompurify';

import { useEffect, useState } from 'react';
import { getTableOfContents } from '../../../../api/EpubReaderApiService';

/** */
export function TableOfContentsComponent(props: any) {
  const [tableOfContents, setTableOfContents] = useState<string | undefined>(
    undefined
  );

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

  let cleanTableOfContentsHtml = DOMPurify.sanitize(
    tableOfContents ?? 'error',
    { USE_PROFILES: { html: true } }
  );

  return (
    <div>
      {<div dangerouslySetInnerHTML={{ __html: tableOfContents ?? "" }} />}
    </div>
  );
}
