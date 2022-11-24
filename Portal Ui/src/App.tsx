import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.scss';
import { CoverPage } from './pages/book/cover-page/CoverPage';
import { LibraryPage } from './pages/library/LibraryPage';
import NoPage from './pages/NoPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<LibraryPage />} />
        <Route path="book/:ebookId" element={<CoverPage />} />
        <Route path="*" element={<NoPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
// https://www.w3schools.com/react/react_router.asp
