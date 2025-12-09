using Philosopher_ServAPI.Core.Models.Entities.Book;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Philosopher_ServAPI.Core.Models.DTOs.Game.Level
{
    public class PostLevelDto
    {
        [Required]
        [JsonPropertyName("text_section_id")]
        public Guid? TextSectionId { get; set; }

        [Required]
        [JsonPropertyName("number")]
        public int? Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = String.Empty;
    }
}
