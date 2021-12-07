namespace DotNETWeekly.Models
{
    public class Record
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Category Category { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
