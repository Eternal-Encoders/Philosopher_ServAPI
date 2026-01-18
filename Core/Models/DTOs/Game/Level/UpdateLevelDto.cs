using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Philosopher_ServAPI.Core.Models.DTOs.Game.Level
{
    public class UpdateLevelDto
    {
        //[Column("number")]
        //public int Number { get; set; }

        [JsonPropertyName("text_section_id")]
        public Guid? TextSectionId { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [MaxLength(1000)]
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
