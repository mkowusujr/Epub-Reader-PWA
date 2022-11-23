import { useState } from "react";
import { addBookMetaData } from "../../../api/EBookMetaDataApiService";

/** Adds ebooks to the local library */
export function AddEBookComponent() {
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
        // .then(response => console.log(JSON.stringify(response)))
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