namespace DotNETWeekly.Options
{
    public class CosmosDbOptions
    {
        public string EndPoint { get; set; }

        public string PrimaryKey { get; set; }

        public string PrimaryReadOnlyKey { get; set; }

        public string DatabaseName { get; set; }

        public string EpisodeSummaryContainer { get; set; }

        public string EpisodeContainer { get; set; }
    }
}
