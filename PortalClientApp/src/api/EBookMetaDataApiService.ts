import axios from 'axios';
import { IEBookMetaData } from '../models/EBookMetaData';

const baseUrl = 'https://localhost:7042/ebookmetadata';

/** Adds an ebook to the user's library */
export async function addBookMetaData(eBookFile: any) {
  try {
    const response = await axios.post(baseUrl, eBookFile);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** Gets the the ebook details for one ebook */
export async function getEBookMetaData(
  bookId: number
): Promise<IEBookMetaData | any> {
  try {
    const response = await axios.get(`${baseUrl}/book/${bookId}`);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** Gets all the books from the library */
export async function getEBookMetaDataList(): Promise<IEBookMetaData[] | any> {
  try {
    const response = await axios.get(baseUrl);
    console.log(JSON.stringify(response));
    return response.data;
  } catch (error) {
    return error;
  }
}

/** Deletes a ebook from the library */
export async function DeleteEBookMetaData(bookId: number) {
  try {
    const response = await axios.delete(`${baseUrl}/book/${bookId}`);
    return response.data;
  } catch (error) {
    return error;
  }
}
// https://medium.com/bb-tutorials-and-thoughts/how-to-make-api-calls-in-react-applications-7758052bf69
