import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import NoPage from './pages/NoPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<NoPage />} />
        <Route index element={<NoPage />} />
        <Route path="*" element={<NoPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
// https://www.w3schools.com/react/react_router.asp
