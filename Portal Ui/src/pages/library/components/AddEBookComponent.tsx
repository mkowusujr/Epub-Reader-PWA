import { useState } from 'react';
import { addBookMetaData } from '../../../api/EBookMetaDataApiService';
import './AddEBookComponent.scss';

/** Adds ebooks to the local library */
export function AddEBookComponent(props: any) {
  const [selectedFile, setSelectedFile] = useState<any>(null);
  const [isFilePicked, setIsFilePicked] = useState(false);

  /** Selects file from user's file system */
  const onFileChange = (event: any) => {
    if (event.target.files[0]) {
      setSelectedFile(event.target.files[0]);
      setIsFilePicked(true);
    }
  };

  /** Uploads files to api */
  const uploadFileToApi = () => {
    const formData = new FormData();
    formData.append('File', selectedFile);

    addBookMetaData(formData)
      .then(response => props.updateShelve())
      .catch(error => console.error(error));
  };

  return (
    <>
      <div className="upload-component">
        <input type="file" name="file" onChange={onFileChange} placeholder="Upload an Epub file"/>
        {isFilePicked ? (
          <div>
            <p>File name: {selectedFile?.name}</p>
            <p>File type: {selectedFile?.type}</p>
            <p>File size: {selectedFile?.size}</p>
          </div>
        ) : (
          <p>Select a file to show details</p>
        )}
      </div>
      <button onClick={uploadFileToApi} disabled={!isFilePicked}>
        Submit
      </button>
    </>
  );
}
