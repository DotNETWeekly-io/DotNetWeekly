import React from 'react';
import { Episode, EpisodeSummary } from './EpisodeData';
import { getEpisodeSummaries } from './EpisodeData';
import {
    Separator,
    Stack,
    IStackTokens,
    DocumentCard,
    IDocumentCardStyles,
    DocumentCardImage,
    ImageFit,
    DocumentCardDetails,
    DocumentCardTitle,
    Spinner,
    SpinnerSize,
} from '@fluentui/react';
import md, { Image } from 'markdown-ast';

const column: number = 3;

const stackTokens: IStackTokens = {
    childrenGap: 12,
};

const cardStyles: IDocumentCardStyles = {
    root: {
        display: 'inline-block',
        marginRight: 20,
        marginBottom: 20,
        width: 320,
    },
};

export interface EpisodeListState {
    episodeSummaries: EpisodeSummary[][];
    loading: boolean;
}

export class EpisodeList extends React.Component<any, EpisodeListState> {
    // eslint-disable-next-line @typescript-eslint/no-useless-constructor

    // eslint-disable-next-line no-empty-pattern
    constructor(any: {}) {
        super(any);
        this.state = {
            episodeSummaries: [],
            loading: true,
        };
    }

    async componentDidMount(): Promise<void> {
        try {
            const episodes = await getEpisodeSummaries();
            const episodesGroup: EpisodeSummary[][] = [];
            episodes.sort((a, b) => {
                if (Number(a.id) < Number(b.id)) {
                    return 1;
                }
                return -1;
            });
            for (let i = 0; i < episodes.length; ) {
                const row: EpisodeSummary[] = [];
                for (let j = 0; j < column && i < episodes.length; j++) {
                    row.push(episodes[i]);
                    i++;
                }
                episodesGroup.push(row);
            }
            this.setState({
                episodeSummaries: episodesGroup,
                loading: false,
            });
        } catch (error) {}
    }

    retrieveImgeSource(episode: Episode): string {
        const astNodes = md(episode.content);
        const imageNode = astNodes.find((p) => p.type === 'image');
        if (!!imageNode) {
            const node = imageNode as Image;
            return node.url;
        }
        return '';
    }

    render(): JSX.Element {
        return (
            <>
                {this.state.loading ? (
                    <Stack
                        verticalFill
                        verticalAlign="center"
                        horizontalAlign="center"
                        styles={{
                            root: {
                                position: 'fixed',
                                top: '50%',
                                left: '50%',
                                transform: 'translate(-50%,-50%)',
                            },
                        }}
                    >
                        <Spinner
                            label="Loading"
                            labelPosition="bottom"
                            size={SpinnerSize.large}
                        ></Spinner>
                    </Stack>
                ) : (
                    <Stack
                        styles={{
                            root: {
                                height: '100px',
                            },
                        }}
                    >
                        {
                            // eslint-disable-next-line array-callback-return
                            this.state.episodeSummaries.map((summaries) => (
                                <>
                                    <Separator />
                                    <Stack
                                        horizontal
                                        horizontalAlign="center"
                                        tokens={stackTokens}
                                    >
                                        {
                                            // eslint-disable-next-line array-callback-return
                                            summaries.map((summary) => (
                                                <DocumentCard
                                                    styles={cardStyles}
                                                    onClickHref={`/episode/${summary.id}`}
                                                >
                                                    <DocumentCardImage
                                                        height={120}
                                                        imageFit={
                                                            ImageFit.centerContain
                                                        }
                                                        imageSrc={summary.image}
                                                    />
                                                    <DocumentCardDetails>
                                                        <DocumentCardTitle
                                                            title={
                                                                summary.title
                                                            }
                                                            shouldTruncate
                                                        ></DocumentCardTitle>
                                                    </DocumentCardDetails>
                                                </DocumentCard>
                                            ))
                                        }
                                    </Stack>
                                </>
                            ))
                        }
                    </Stack>
                )}
            </>
        );
    }
}
