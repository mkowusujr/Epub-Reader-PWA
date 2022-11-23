import { useEffect, useState } from 'react';
import {
  addBookMetaData,
  getEBookMetaDataList
} from '../../../api/EBookMetaDataApiService';
import { IEBookMetaData } from '../../../core/interfaces/EBookMetaData';

function AddEBookComponent() {
  const [selectedFile, setSelectedFile] = useState<any>(null);
  const [isFilePicked, setIsFilePicked] = useState(false);

  /** Selects file from user's file system */
  const onFileChange = (event: any) => {
    setSelectedFile(event?.target?.files[0]);
    setIsFilePicked(true);
  };

  /** Uploads files to api */
  const uploadFileToApi = () => {
    const formData = new FormData();
    formData.append('File', selectedFile);

    addBookMetaData(formData)
      .then(response => console.log(JSON.stringify(response)))
      .catch(error => console.error(error));
  };

  return (
    <div>
      <input type="file" name="file" onChange={onFileChange} />
      {isFilePicked ? (
        <div>
          <p>File name: {selectedFile?.name}</p>
          <p>File type: {selectedFile?.type}</p>
          <p>File size: {selectedFile?.size}</p>
        </div>
      ) : (
        <p>Select a file to show details</p>
      )}
      <button onClick={uploadFileToApi}>Submit</button>
    </div>
  );
}

/** The Compnent for displaying the ebooks */
function ShelveComponent() {
  const [eBooks, setEBooks] = useState<IEBookMetaData[]>([]);

  /** Calls the api to get all the saved ebooks */
  const fetchEBooksFromApi = () => {
    getEBookMetaDataList().then((fetchedEBooks: IEBookMetaData[]) => {
      console.log(fetchedEBooks)
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
          <li key={eBook.id}>{eBook.id}</li>
        ))}
      </ul>
    </div>
  );
}

/** The Library Page */
export function LibraryPage() {
  return (
    <>
      <AddEBookComponent />
      <ShelveComponent />
    </>
  );
}

// https://www.w3schools.com/react/react_components.asp
// https://www.w3schools.com/react/react_lists.asp
// https://www.w3schools.com/react/react_conditional_rendering.asp
// https://www.w3schools.com/react/react_forms.asp
// https://www.w3schools.com/react/react_events.asp
// https://www.w3schools.com/react/react_sass_styling.asp
