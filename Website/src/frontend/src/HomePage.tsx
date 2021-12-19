import React from 'react';
import { getEpisodes } from './EpisodeData';
import { EpisodeList } from './EpisodeList';
import styles from './HomePage.module.css';
export class HomePage extends React.Component {
    state = {
        episodes: [],
    };

    componentDidMount() {
        getEpisodes().then((episodes) => {
            if (!!episodes) {
                this.setState({ episodes: episodes });
            }
        });
    }

    render() {
        return <EpisodeList data={this.state.episodes}></EpisodeList>;
    }
}

export default HomePage;
