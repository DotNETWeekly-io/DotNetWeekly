import React from 'react';
import { getEpisodes } from './EpisodeData';
import { EpisodeList } from './EpisodeList';
export class HomePage extends React.Component {
    state = {
        episodes: [],
    };

    componentDidMount() {
        getEpisodes().then((episodes) => {
            if (!!episodes) {
                episodes.sort((a, b) =>
                    a.createTime > b.createTime
                        ? -1
                        : a.createTime < b.createTime
                        ? 1
                        : 0,
                );
                this.setState({ episodes: episodes });
            }
        });
    }

    render() {
        return <EpisodeList data={this.state.episodes}></EpisodeList>;
    }
}

export default HomePage;
