import { Stack } from '@fluentui/react';
import React from 'react';
import { EpisodeList } from './EpisodeList';
import Header from './Header';
import Footer from './Footer';

export class HomePage extends React.Component {
    render(): JSX.Element {
        return (
            <Stack verticalFill>
                <Header></Header>
                <EpisodeList></EpisodeList>
                <Footer></Footer>
            </Stack>
        );
    }
}

export default HomePage;
