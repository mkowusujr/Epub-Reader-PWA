/** */
export function EBookCoverInfoComponent(props: any) {
  return (
    <div className="cover-page-info">
      <img src={`data:image/jpeg;base64,${props.eBook.coverImage}`} />
      <h1>{props.eBook.title}</h1>
      <h2>{props.eBook.author}</h2>
    </div>
  );
}
