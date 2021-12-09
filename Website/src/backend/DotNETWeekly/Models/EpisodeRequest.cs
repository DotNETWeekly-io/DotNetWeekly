using System.ComponentModel.DataAnnotations;
namespace DotNETWeekly.Models
{
    public class EpisodeRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Introduction { get; set; }

    }
}
