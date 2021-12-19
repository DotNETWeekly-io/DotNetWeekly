import React from 'react';
import './App.css';
import Header from './Header';
import Footer from './Footer';
import HomePage from './HomePage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { EpisodePage } from './EpisodePage';

export class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <div className="App">
                    <Header />
                    <Routes>
                        <Route path="" element={<HomePage />} />
                        <Route
                            path="episode/:episodeId"
                            element={<EpisodePage />}
                        />
                    </Routes>
                    <Footer />
                </div>
            </BrowserRouter>
        );
    }
}

export default App;
