import { DeleteEBookMetaData } from '../../../api/EBookMetaDataApiService';
import { IEBookMetaData } from '../../../models/EBookMetaData';
import './DeleteEBookButtonComponent.scss';

export function DeleteEBookButtonComponent(props: any) {
  const eBook: IEBookMetaData = props.eBook;
  const deleteEBook = (eBookId: number) => {
    DeleteEBookMetaData(eBookId).catch(error => console.error(error));
  };

  return (
    <button
      onClick={() => deleteEBook(eBook.id ?? -1)}
      className="delete-button"
    >
      X
    </button>
  );
}
