import { http } from './http';
export interface Episode {
    id: string;
    title: string;
    content: string;
    createTime: Date;
}

export interface EpisodeSummary {
    id: string;
    title: string;
    image: string;
    digist: string;
    createTime: Date;
}

export async function getEpisodeSummaries(): Promise<EpisodeSummary[]> {
    const result = await http<EpisodeSummary[]>({ path: '/episodes/summary' });
    if (result.ok && result.body) {
        return result.body;
    } else {
        return [];
    }
}

export async function getEpisodeById(id: number): Promise<Episode | null> {
    const result = await http<Episode>({ path: `/episodes/${id}` });
    if (result.ok && result.body) {
        return result.body;
    } else {
        return null;
    }
}
