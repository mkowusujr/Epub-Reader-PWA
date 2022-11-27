import { Link } from "react-router-dom";
import { DeleteEBookButtonComponent } from "./DeleteEBookButtonComponent";
import './EBookComponent.scss'

/** The component to display an ebook on the shelve component */
export function EBookComponent(props: any) {
  let eBook = props.eBook;

  return (
    <div className="ebook-component">
      <DeleteEBookButtonComponent eBook={eBook} updateShelve={props.updateShelve} />
      <Link to={`/book-id/${eBook.id}`} className="ebook-component">
        <img
          src={`data:image/jpeg;base64,${eBook.coverImage}`}
          alt="Book Cover"
        />
        <p>{eBook.title}</p>
      </Link>
    </div>
  );
}
