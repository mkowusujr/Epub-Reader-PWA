import { useState, useEffect } from 'react';
import { getEBookMetaDataList } from '../../../api/EBookMetaDataApiService';
import { IEBookMetaData } from '../../../models/EBookMetaData';
import { AddEBookComponent } from './AddEBookComponent';
import { EBookComponent } from './EBookComponent';
import './ShelveComponent.scss';

/** The Compnent for displaying the ebooks */
export function ShelveComponent() {
  const [eBooks, setEBooks] = useState<IEBookMetaData[]>([]);

  /** Calls the api to get all the saved ebooks */
  const fetchEBooksFromApi = () => {
    getEBookMetaDataList().then((fetchedEBooks: IEBookMetaData[]) => {
      setEBooks(fetchedEBooks);
    });
  };

  useEffect(() => {
    fetchEBooksFromApi();
  }, []);

  return (
    <div>
      {/* <h1>EBook Library</h1> */}
      <ul className="shelve-items">
        <li>
          <AddEBookComponent updateShelve={fetchEBooksFromApi} />
        </li>
        {eBooks.map(eBook => (
          <li key={eBook.id}>
            <EBookComponent eBook={eBook} updateShelve={fetchEBooksFromApi} />
          </li>
        ))}
      </ul>
    </div>
  );
}
