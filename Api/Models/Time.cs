using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Time
    {
        public int Id { get; set; }

        [Required]
        public TimeOnly FeedingTime { get; set; }

    }
}
