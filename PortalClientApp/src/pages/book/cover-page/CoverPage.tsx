import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getEBookMetaData } from '../../../api/EBookMetaDataApiService';
import { IEBookMetaData } from '../../../models/EBookMetaData';
import NoPage from '../../NoPage';
import { EBookCoverInfoComponent } from './components/EBookCoverInfoComponent';
import { TableOfContentsComponent } from './components/TableOfContentsComponent';
import './CoverPage.scss';

/** Displays some of the basic book information */
export function CoverPage() {
  const [eBook, setEBook] = useState<IEBookMetaData | undefined>(undefined);
  const { ebookId } = useParams();

  const fetchEBookFromApi = (ebookIdParam: number) => {
    getEBookMetaData(ebookIdParam)
      .then(fetchedEBook => setEBook(fetchedEBook))
      .catch(error => console.error(error));
  };

  useEffect(() => {
    if (ebookId) {
      fetchEBookFromApi(+ebookId);
      document.title = eBook?.title ?? "Cover Page";
    }
  }, []);

  return (
    <>
      {eBook ? (
        <div className="cover-page">
          <EBookCoverInfoComponent eBook={eBook} />
          <TableOfContentsComponent eBook={eBook} />
        </div>
      ) : (
        <NoPage />
      )}
    </>
  );
}
