import React from 'react';
import { Episode } from './EpisodeData';
import { getEpisodes } from './EpisodeData';
import { Separator, Stack, IStackTokens, DocumentCard, IDocumentCardStyles,DocumentCardImage,ImageFit,DocumentCardDetails,DocumentCardTitle } from '@fluentui/react';
import md, { Image } from 'markdown-ast'

const column: number = 3;

const stackTokens: IStackTokens = {
    childrenGap: 12,
}

const cardStyles: IDocumentCardStyles = {
    root: { display: 'inline-block', marginRight: 20, marginBottom: 20, width: 320 },
};
export class EpisodeList extends React.Component {
    // eslint-disable-next-line @typescript-eslint/no-useless-constructor

    private episodes: Episode[][] = [];

    constructor() {
        super({})
    }

    async componentDidMount(): Promise<void> {
        try {
            const episodes = await getEpisodes();
            episodes.sort((a, b) => {
                if (a.createTime < b.createTime) {
                    return 1;
                }
                return -1;
            })
            for (let i = 0; i < episodes.length;) {
                const row: Episode[] = [];
                for (let j = 0; j < column && i < episodes.length; j++) {
                    row.push(episodes[i]);
                    i++;
                }
                this.episodes.push(row);
            }
        } catch (error) {

        }
    }

    retrieveImgeSource(episode: Episode): string {
        const astNodes = md(episode.content);
        const imageNode = astNodes.find((p) => p.type === "image");
        if (!!imageNode) {
            const node = imageNode as Image;
            return node.ref;
        }

        return '';
    }

    render(): JSX.Element {
        return (
            <Stack>
                {
                    // eslint-disable-next-line array-callback-return
                    this.episodes.map((episodes) => {
                        (
                            <Separator>
                                <Stack horizontal horizontalAlign="center" tokens={stackTokens}>
                                    {
                                        // eslint-disable-next-line array-callback-return
                                        episodes.map((episode) => {
                                            <DocumentCard
                                                styles={cardStyles}
                                                onClickHref={`/episode/${episode.id}`}
                                            >
                                                <DocumentCardImage height={120} imageFit={ImageFit.centerContain} imageSrc={this.retrieveImgeSource(episode)} />
                                                <DocumentCardDetails>
                                                    <DocumentCardTitle title={episode.title} shouldTruncate></DocumentCardTitle>
                                                </DocumentCardDetails>
                                            </DocumentCard>
                                        })
                                    }
                                </Stack>
                            </Separator>
                        )
                    })
                }
            </Stack>
        )
    }
}
