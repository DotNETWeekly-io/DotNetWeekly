import React from 'react';
import { Episode, getEpisodeById } from './EpisodeData';
import { useParams } from 'react-router';
import ReactMarkdown from 'react-markdown';
import styles from './EpisodePage.module.css';
import './EpisodePage.module.css';
import { Stack } from '@fluentui/react';
import Header from './Header';
import Footer from './Footer';

export const EpisodePage = () => {
    const [episode, setEpisode] = React.useState<Episode | null>(null);
    const { episodeId } = useParams();

    React.useEffect(() => {
        const doGetEpisode = async (episodeId: number) => {
            const foundEpisode = await getEpisodeById(episodeId);
            setEpisode(foundEpisode);
        };
        if (episodeId) {
            doGetEpisode(Number(episodeId));
        }
    }, [episodeId]);

    return (
        <div className={styles.episodePage}>
            <Stack verticalFill>
                <Header></Header>
                <ReactMarkdown>{episode ? episode.content : ''}</ReactMarkdown>
                <Footer></Footer>
            </Stack>

        </div>
    );
};

export default EpisodePage;
