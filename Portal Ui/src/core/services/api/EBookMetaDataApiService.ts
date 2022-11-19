import axios from 'axios';
import { IEBookMetaData } from '../../interfaces/EBookMetaData';

const baseUrl = 'localhost:7042/';

/** */
export async function addBookMetaData(eBookMetaData: IEBookMetaData) {
  try {
    const response = await axios.post(baseUrl, eBookMetaData);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** */
export async function getEBookMetaData(bookId: number) {
  try {
    const response = await axios.get(`${baseUrl}/book/${bookId}`);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** */
export async function getEBookMetaDataList() {
  try {
    const response = await axios.get(baseUrl);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** */
export async function DeleteEBookMetaData(bookId: number) {
  try {
    const response = await axios.delete(`${baseUrl}/book/${bookId}`);
    return response.data;
  } catch (error) {
    return error;
  }
}
// https://medium.com/bb-tutorials-and-thoughts/how-to-make-api-calls-in-react-applications-7758052bf69
