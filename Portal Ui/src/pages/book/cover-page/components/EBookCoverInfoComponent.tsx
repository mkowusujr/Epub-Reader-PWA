/** */
export function EBookCoverInfoComponent(props: any) {
  return (
    <div>
      <h1>{props.eBook.title}</h1>
      <h2>{props.eBook.author}</h2>
      <img src={`data:image/jpeg;base64,${props.eBook.coverImage}`} />
    </div>
  );
}
