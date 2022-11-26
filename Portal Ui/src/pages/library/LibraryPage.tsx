import { AddEBookComponent } from './components/AddEBookComponent';
import { ShelveComponent } from './components/ShelveComponent';
import './LibraryPage.scss';

/** The Library Page */
export function LibraryPage() {
  document.title = "Portal EReader - Library";

  return (
    <div className='library-page'>
      <AddEBookComponent />
      <ShelveComponent />
    </div>
  );
}

// https://www.w3schools.com/react/react_components.asp
// https://www.w3schools.com/react/react_lists.asp
// https://www.w3schools.com/react/react_conditional_rendering.asp
// https://www.w3schools.com/react/react_forms.asp
// https://www.w3schools.com/react/react_events.asp
// https://www.w3schools.com/react/react_sass_styling.asp
