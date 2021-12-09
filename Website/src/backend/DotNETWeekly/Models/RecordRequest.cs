using System.ComponentModel.DataAnnotations;
namespace DotNETWeekly.Models
{
    public class RecordRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }


        [Required]
        public Category Category { get; set; }

        [Required]
        public int EpisodeId { get; set; }
    }
}
