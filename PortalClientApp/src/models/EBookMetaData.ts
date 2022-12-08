export interface IEBookMetaData {
  id?: number;
  title: string;
  author: string;
  coverImage: BigInt64Array;
  isMarkAsFavorite: boolean;
  tableOfContents: string;
}
