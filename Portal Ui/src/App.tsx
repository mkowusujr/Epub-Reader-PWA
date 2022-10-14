import { useState } from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import reactLogo from './assets/react.svg'
import './App.css'
import NoPage from './pages/NoPage';

function App() {
  const [count, setCount] = useState(0)

  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<NoPage/>} />
        <Route index element={<NoPage/>} />
        <Route path='*' element={<NoPage/>} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
