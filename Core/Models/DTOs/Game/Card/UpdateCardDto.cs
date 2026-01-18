using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Philosopher_ServAPI.Core.Models.DTOs.Game.Card
{
    public class UpdateCardDto
    {
        [JsonPropertyName("level_id")]
        public Guid? LevelId { get; set; }

        [Range(1, int.MaxValue)]
        [JsonPropertyName("number")]
        public int? Number { get; set; }

        [Length(0, 1000)]
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [Length(0, 256)]
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [Length(0, 256)]
        [JsonPropertyName("first_choice_text")]
        public string? FirstChoiceText { get; set; }

        [Length(0, 256)]
        [JsonPropertyName("second_choice_text")]
        public string? SecondChoiceText { get; set; }

        [Range(-100, +100)]
        [JsonPropertyName("first_hum_delta")]
        public int? HumanDelta1 { get; set; }

        [Range(-100, +100)]
        [JsonPropertyName("second_hum_delta")]
        public int? HumanDelta2 { get; set; }

        [Range(-100, +100)]
        [JsonPropertyName("first_rob_delta")]
        public int? RobotDelta1 { get; set; }

        [Range(-100, +100)]
        [JsonPropertyName("second_rob_delta")]
        public int? RobotDelta2 { get; set; }

        [JsonPropertyName("image_link")]
        public string? ImageLink { get; set; }
    }
}
