export interface IEBookMetaData {
  id?: number;
  title: string;
  author: string;
  coverImage: BigInt64Array;
  filePath: string;
  isMarkAsFavorite: boolean;
}
