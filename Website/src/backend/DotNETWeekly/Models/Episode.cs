namespace DotNETWeekly.Models
{
    using System;
    public class Episode
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
