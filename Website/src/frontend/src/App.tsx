import React from 'react';
import './App.css';
import Header from './Header';
import Footer from './Footer';

export class App extends React.Component {
    render() {
        return (
            <div className="App">
                <Header />
                <Footer />
            </div>
        );
    }
}

export default App;
