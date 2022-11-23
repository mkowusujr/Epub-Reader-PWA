/** The component to display an ebook on the shelve component */
export function EBookComponent(props: any) {
    let eBook = props.eBook;
  
    return (
      <a>
        <img
          src={`data:image/jpeg;base64,${eBook.coverImage}`}
          alt="Book Cover"
        />
        <p>{eBook.title}</p>
      </a>
    );
  }