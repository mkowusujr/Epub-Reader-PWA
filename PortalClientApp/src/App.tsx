import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.scss';
import { BookSectionPage } from './pages/book/book-section/BookSectionPage';
import { CoverPage } from './pages/book/cover-page/CoverPage';
import { LibraryPage } from './pages/library/LibraryPage';
import NoPage from './pages/NoPage';
import { Header } from './shared/layouts/header/components/Header';
import { NavBar } from './shared/layouts/navbar/NavBar';

function App() {
  return (
    <>
      <BrowserRouter>
        <Header />
        <section>
          <NavBar />
          <Routes>
            {/* <Route path="/" element={<NavBar />}> */}
            <Route index element={<LibraryPage />} />
            <Route
              path="book-id/:ebookId/chapter-id/:chapterId"
              element={<BookSectionPage />}
            />
            <Route path="book-id/:ebookId" element={<CoverPage />} />
            <Route path="*" element={<NoPage />} />
            {/* </Route> */}
          </Routes>
        </section>
      </BrowserRouter>
    </>
  );
}

export default App;
// https://www.w3schools.com/react/react_router.asp
