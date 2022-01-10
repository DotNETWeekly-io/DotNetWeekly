namespace DotNETWeekly.Models
{
    public class Episode
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<Record> Records { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
