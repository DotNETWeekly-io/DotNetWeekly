using System.ComponentModel.DataAnnotations;
namespace DotNETWeekly.Models
{
    public class EpisodeRequest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
