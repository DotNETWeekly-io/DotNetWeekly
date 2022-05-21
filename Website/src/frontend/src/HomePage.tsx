import { DocumentCard, Label, Stack, StackItem, IDocumentCardStyles, IIconProps, initializeIcons, DocumentCardImage, ImageFit, DocumentCardDetails, DocumentCardTitle, IStackTokens, Separator } from '@fluentui/react';
import React from 'react';
import { getEpisodes } from './EpisodeData';
import { EpisodeList } from './EpisodeList';
import md from 'markdown-ast'
import dotnet from './dotnet-bot.svg';


const cardStyles: IDocumentCardStyles = {
    root: { display: 'inline-block', marginRight: 20, marginBottom: 20, width: 320 },
};

const oneNoteIconProps: IIconProps = {
    iconName: 'News',
    styles: { root: { color: '#000', fontSize: '80px', width: '80px', height: '80px' } },
};

const stackTokens: IStackTokens = {
    childrenGap: 12,
}

export class HomePage extends React.Component {

    constructor({}) {
        super({});
        initializeIcons();
        const ast = md("123")
    }

    render(): JSX.Element {
        return (
            <>
                <Stack verticalFill>
                    <Stack horizontal horizontalAlign="space-between" verticalAlign="center" styles={{
                        root: {
                            height: "40px",
                            margin: '1px',
                            "background-color": "rgb(243, 237, 229)",
                            "font-weight": "bold",
                        }
                    }}>
                        <StackItem>
                            <img src={dotnet} alt="Logo" width="35px" style={{ marginLeft: "10px" }} />
                        </StackItem>
                        <StackItem>
                            <Label>.NET 周刊</Label>
                        </StackItem>
                        <StackItem>
                            <span></span>
                        </StackItem>
                    </Stack>
                    <Stack>
                        <Separator></Separator>
                        <Stack horizontal horizontalAlign='center' tokens={stackTokens}>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.centerContain} imageSrc="https://dotnetweeklypics.blob.core.windows.net/014/cosmosdb.jpeg"/>
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                        </Stack>
                        <Separator></Separator>
                        <Stack horizontal horizontalAlign='center' tokens={stackTokens}>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                        </Stack>
                        <Separator></Separator>
                        <Stack horizontal horizontalAlign='center' tokens={stackTokens}>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                            <DocumentCard
                                styles={cardStyles}
                                onClickHref="https://baidu.com"
                            >
                                <DocumentCardImage height={120} imageFit={ImageFit.cover} iconProps={oneNoteIconProps} />
                                <DocumentCardDetails>
                                    <DocumentCardTitle title=".NET 周刊第 1 期" shouldTruncate></DocumentCardTitle>
                                </DocumentCardDetails>
                            </DocumentCard>
                        </Stack>


                    </Stack>
                    <Stack horizontal horizontalAlign="center" verticalAlign="baseline" styles={{
                        root: {
                            left: 0,
                            bottom: 0,
                            width: "100%",
                            height: "30px",
                            position: "fixed",
                            "background-color": "#f7f8fa",
                            "text-align": "center",
                            "font-size": "12px",
                            "font-style": "italic",
                        }
                    }}>
                        <p>
                            本周刊开源，(GitHub:
                            <a
                                href="https://github.com/gaufung/DotNetWeekly"
                                target="_blank"
                                rel="noreferrer"
                            >
                                gaufung/DotNetWeekly
                            </a>
                            )，欢迎提交 issue 投稿。
                        </p>
                    </Stack>
                </Stack>
            </>
        )
    }
}

export default HomePage;
