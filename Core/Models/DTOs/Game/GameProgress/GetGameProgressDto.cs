using Philosopher_ServAPI.Core.Models.Entities.Game;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Philosopher_ServAPI.Core.Models.DTOs.Game.GameProgress
{
    public class GetGameProgressDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("level_id")]
        public required Guid LevelId { get; set; }

        [JsonPropertyName("game_ended")]
        public bool GameEnded { get; set; } = false;

        [JsonPropertyName("human")]
        public int Humanity { get; set; } = 50;

        [JsonPropertyName("robot")]
        public int Robotification { get; set; } = 50;

        [JsonPropertyName("step_number")]
        public int StepNumber { get; set; } = 1;

        [JsonPropertyName("level_ending")]
        public LevelEnding? LevelEnding { get; set; }

        [JsonPropertyName("last_card")]
        public Entities.Game.Card? LastCard { get; set; }

    }
}
