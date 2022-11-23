import { useState, useEffect } from "react";
import { getEBookMetaDataList } from "../../../api/EBookMetaDataApiService";
import { IEBookMetaData } from "../../../core/interfaces/EBookMetaData";
import { EBookComponent } from "./EBookComponent";

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
        <h1>EBook Library</h1>
        <ul>
          {eBooks.map(eBook => (
            <li key={eBook.id}>
              <EBookComponent eBook={eBook} />
            </li>
          ))}
        </ul>
      </div>
    );
  }