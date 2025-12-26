using System.ComponentModel.DataAnnotations;

namespace Philosopher_ServAPI.Core.Models.DTOs.Game.GameProgress
{
    public class PostGameProgressDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Choice { get; set; }
    }
}
