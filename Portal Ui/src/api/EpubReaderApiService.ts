import axios from 'axios';

const baseUrl = 'https://localhost:7042/epubreader';

/** Gets the title of the ebook */
export async function getTitle(bookId: number) {
  try {
    const response = await axios.get(`${baseUrl}/read-book/${bookId}/title`);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** Gets the author of the ebook */
export async function getAuthors(bookId: number) {
  try {
    const response = await axios.get(`${baseUrl}/read-book/${bookId}/authors`);
    return response.data;
  } catch (error) {
    return error;
  }
}

/** Gets the table of contents of the ebook */
export async function getTableOfContents(bookId: number) {
  try {
    const response = await axios.get(
      `${baseUrl}/read-book/${bookId}/tableofcontents`
    );
    return response.data;
  } catch (error) {
    return error;
  }
}

export async function getCoverImage(bookId: number) {
  try {
    const response = await axios.get(
      `${baseUrl}/read-book/${bookId}/coverimage`
    );
    return response.data;
  } catch (error) {
    return error;
  }
}

/** Gets a page from the ebook */
export async function getPage(bookId: number, sectionName: string) {
  try {
    const response = await axios.get(
      `${baseUrl}/read-book/${bookId}/sections/${sectionName}`
    );
    return response.data;
  } catch (error) {
    return error;
  }
}
