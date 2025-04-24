using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class TimeDto
    {
        [Required]
        public TimeOnly FeedingTime { get; set; }
    }
}
