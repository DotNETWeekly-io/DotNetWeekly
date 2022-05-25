import React from 'react';
import { Episode, getEpisodeById } from './EpisodeData';
import { useParams } from 'react-router';
import ReactMarkdown from 'react-markdown';
import styles from './EpisodePage.module.css';
import './EpisodePage.module.css';
import {
    Stack,
    Spinner,
    SpinnerSize,
} from '@fluentui/react';
import Header from './Header';
import Footer from './Footer';
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter';


export const EpisodePage = () => {
    const [episode, setEpisode] = React.useState<Episode | null>(null);
    const [loading, setLoading] = React.useState<boolean>(true);
    const { episodeId } = useParams();

    React.useEffect(() => {
        const doGetEpisode = async (episodeId: number) => {
            const foundEpisode = await getEpisodeById(episodeId);
            setEpisode(foundEpisode);
            setLoading(false);
        };
        if (episodeId) {
            doGetEpisode(Number(episodeId));
        }
    }, [episodeId]);

    return (
        <Stack verticalFill>
            <Header></Header>
            {
                loading ? (<Stack verticalFill verticalAlign="center" horizontalAlign="center" styles={{
                    root: {
                        position: "fixed",
                        top: "50%",
                        left: "50%",
                        transform: 'translate(-50%,-50%)',
                    }
                }}>
                    <Spinner label="Loading" labelPosition="bottom" size={SpinnerSize.large}>
                    </Spinner>
                </Stack>) : (<div className={styles.episodePage}>
                    <ReactMarkdown
                        children={episode ? episode.content : ''}
                        components={{
                            code({ node, inline, className, children, ...props }) {
                                const match = /language-(\w+)/.exec(className || '')
                                return !inline && match ? (
                                    <SyntaxHighlighter
                                        children={String(children).replace(/\n$/, '')}
                                        language={match[1]}
                                    />
                                ) : (
                                    <code className={className} {...props}>
                                        {children}
                                    </code>
                                )
                            }
                        }}
                    />
                </div>)
            }
            <Footer></Footer>
        </Stack>
    );
};

export default EpisodePage;
