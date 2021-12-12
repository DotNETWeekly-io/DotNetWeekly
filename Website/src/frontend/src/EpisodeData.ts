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
    introduction: string;
    records: Record[];
    createTime: Date;
}

const episodes: Episode[] = [
    {
        id: 1,
        title: '.NET 周刊第 1 期',
        introduction:
            '**周刊** 第 `1` 期\n![](https://www.dotnetconf.net/img/world-of-dotnet.svg)',
        records: [
            {
                id: 1,
                title: 'Open .NET 来了?',
                link: 'https://www.infoqP.cn/article/ut0oDCTQmT7Sdu5Ega2k',
                content:
                    '上个月一群人 Fork 了 .NET 平台下的开源项目，并且成立 Open .NET 组织，导火索是微软在 .NET 6 发布的时候，将 CLI 工具中删除了 Hot Reload 相关的代码，使它变成了 Visual Studio 2022 独占功能。这个引发了微软在开发者社区的信任危机，他们希望借助 Open .NET 来摆脱微软对 .NET 的掌控。',
                episodeId: 1,
                category: Category.News,
                createTime: new Date(),
            },
            {
                id: 2,
                title: '.NET Conf 回顾',
                link: 'https://devblogs.microsoft.com/dotnet/net-conf-2021-recap-videos-slides-demos-and-more/',
                content:
                    '上个月微软举办了一年一度的 `.NET Conf` , 在会议中发布了 `.NET 6` 和 `Visual Studio`, 这篇文章可以帮你回顾一下这次会议和其中的亮点。',
                episodeId: 1,
                category: Category.News,
                createTime: new Date(),
            },
            {
                id: 3,
                title: '如何给 .NET 社区做贡献',
                link: 'https://rion.io/2017/04/28/contributing-to-net-for-dummies/',
                content:
                    '作者分享了如何给 .NET 社区做共享，那怕你仍然还是一个新手。作者提出了一般人会提出的四个问题，并且一一回答它们',
                episodeId: 1,
                category: Category.Article,
                createTime: new Date(),
            },
            {
                id: 4,
                title: '单元测试框架',
                link: 'https://github.com/moq/moq4',
                content:
                    '在软件开发中，单元测试是必不可少的部分。但是代码中存在一些外部的依赖，因此需要对它们进行 **Mock**。在 `.NET` 平台，最有名的框架就是 `Moq`，借助它可以帮助我们有效编写单元测试，并且辅助我们遵循面向接口编程原则。',
                episodeId: 1,
                category: Category.OpenSource,
                createTime: new Date(),
            },
        ],
        createTime: new Date(),
    },
    {
        id: 2,
        title: '.NET 周刊第 2 期',
        introduction:
            '**周刊** 第 `2` 期\n![](https://www.dotnetconf.net/img/world-of-dotnet.svg)',
        records: [
            {
                id: 5,
                title: 'Open .NET 来了?',
                link: 'https://www.infoqP.cn/article/ut0oDCTQmT7Sdu5Ega2k',
                content:
                    '上个月一群人 Fork 了 .NET 平台下的开源项目，并且成立 Open .NET 组织，导火索是微软在 .NET 6 发布的时候，将 CLI 工具中删除了 Hot Reload 相关的代码，使它变成了 Visual Studio 2022 独占功能。这个引发了微软在开发者社区的信任危机，他们希望借助 Open .NET 来摆脱微软对 .NET 的掌控。',
                episodeId: 2,
                category: Category.News,
                createTime: new Date(),
            },
            {
                id: 6,
                title: '.NET Conf 回顾',
                link: 'https://devblogs.microsoft.com/dotnet/net-conf-2021-recap-videos-slides-demos-and-more/',
                content:
                    '上个月微软举办了一年一度的 `.NET Conf` , 在会议中发布了 `.NET 6` 和 `Visual Studio`, 这篇文章可以帮你回顾一下这次会议和其中的亮点。',
                episodeId: 1,
                category: Category.News,
                createTime: new Date(),
            },
            {
                id: 7,
                title: '如何给 .NET 社区做贡献',
                link: 'https://rion.io/2017/04/28/contributing-to-net-for-dummies/',
                content:
                    '作者分享了如何给 .NET 社区做共享，那怕你仍然还是一个新手。作者提出了一般人会提出的四个问题，并且一一回答它们',
                episodeId: 2,
                category: Category.Article,
                createTime: new Date(),
            },
            {
                id: 8,
                title: 'FluentAssertions',
                link: 'https://fluentassertions.com/',
                content: '不管在什么单元测试框架中，单元测试的形式一般如下',
                episodeId: 2,
                category: Category.OpenSource,
                createTime: new Date(),
            },
        ],
        createTime: new Date(),
    },
];

const wait = async (ms: number): Promise<void> => {
    return new Promise((resolve) => setTimeout(resolve, ms));
};

export const getEpisodes = async (): Promise<Episode[] | null> => {
    await wait(500);
    return episodes.length === 0 ? null : episodes;
};

export const getEpisodeById = async (id: number): Promise<Episode | null> => {
    await wait(500);
    const results = episodes.filter((e) => e.id === id);
    return results.length === 0 ? null : results[0];
};
