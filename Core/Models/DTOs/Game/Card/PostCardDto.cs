using Philosopher_ServAPI.Core.Models.Entities.Game;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Philosopher_ServAPI.Core.Models.DTOs.Game.Card
{
    public class PostCardDto
    {
        [Required]
        [JsonPropertyName("level_id")]
        public Guid? LevelId { get; set; }

        [JsonPropertyName("number")]
        public int? Number { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = String.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;

        [JsonPropertyName("first_choice_text")]
        public string FirstChoiceText { get; set; } = String.Empty;

        [JsonPropertyName("second_choice_text")]
        public string SecondChoiceText { get; set; } = String.Empty;

        [Required]
        [JsonPropertyName("first_hum_delta")]
        public required int HumanityDelta1 { get; set; }

        [Required]
        [JsonPropertyName("second_hum_delta")]
        public required int HumanityDelta2 { get; set; }

        [Required]
        [JsonPropertyName("first_rob_delta")]
        public required int RobotificationDelta1 { get; set; }

        [Required]
        [JsonPropertyName("second_rob_delta")]
        public required int RobotificationDelta2 { get; set; }

        [JsonPropertyName("image_link")]
        public string ImageLink { get; set; } = String.Empty;
    }
}
