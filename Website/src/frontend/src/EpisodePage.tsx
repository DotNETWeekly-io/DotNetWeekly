import React from 'react';
import { Category, Episode, getEpisodeById, Record } from './EpisodeData';
import { useParams } from 'react-router';
import ReactMarkdown from 'react-markdown';

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

    const expandRecords = (records: Record[], category: Category): string => {
        let rawContent = '';
        if (category === Category.News) {
            rawContent += `## 行业资讯\n\n`;
        } else if (category === Category.Article) {
            rawContent += `## 文章推荐\n\n`;
        } else if (category === Category.OpenSource) {
            rawContent += `## 开源项目\n\n`;
        }
        for (let index = 0; index < records.length; index++) {
            const record = records[index];
            rawContent += `${index + 1}、[${record.title}](${record.link})\n\n`;
            rawContent += `${record.content}\n\n`;
        }
        return rawContent;
    };

    const expandEpisode = (epi: Episode | null): string => {
        if (!epi) {
            return 'Not Found';
        } else {
            let rawContent = '';
            rawContent += `## ${epi.title}\n\n`;
            rawContent += `${epi.introduction}\n\n`;
            let records = epi.records.filter(
                (r) => r.category === Category.News,
            );
            if (records) {
                rawContent += expandRecords(records, Category.News);
            }
            records = epi.records.filter(
                (r) => r.category === Category.Article,
            );
            if (records) {
                rawContent += expandRecords(records, Category.Article);
            }
            records = epi.records.filter(
                (r) => r.category === Category.OpenSource,
            );
            if (records) {
                rawContent += expandRecords(records, Category.OpenSource);
            }
            return rawContent;
        }
    };

    return <ReactMarkdown>{expandEpisode(episode)}</ReactMarkdown>;
};

export default EpisodePage;
