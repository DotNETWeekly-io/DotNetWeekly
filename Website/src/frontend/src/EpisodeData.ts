import { http } from './http';
export enum Category {
    News,
    OpenSource,
    Article,
}

export interface Record {
    id: number;
    title: string;
    link: string;
    content: string;
    episodeId: number;
    category: Category;
    createTime: Date;
}

export interface Episode {
    id: number;
    title: string;
    content: string;
    records: Record[];
    createTime: Date;
}

export async function getEpisodes(): Promise<Episode[]> {
    const result = await http<Episode[]>({ path: '/episodes' });
    if (result.ok && result.body) {
        return result.body;
    } else {
        return [];
    }
};

export async function getEpisodeById(id: number): Promise<Episode | null> {
    const result = await http<Episode>({ path: `/episodes/${id}` });
    if (result.ok && result.body) {
        return result.body;
    } else {
        return null;
    }
};
