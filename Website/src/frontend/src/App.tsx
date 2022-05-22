import React from 'react';
import './App.css';
import HomePage from './HomePage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { EpisodePage } from './EpisodePage';
import { NotFoundPage } from './NotFoundPage';

export class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <div className="App">
                    <Routes>
                        <Route path="" element={<HomePage />} />
                        <Route
                            path="episode/:episodeId"
                            element={<EpisodePage />}
                        />
                        <Route path="*" element={<NotFoundPage />} />
                    </Routes>
                </div>
            </BrowserRouter>
        );
    }
}

export default App;
