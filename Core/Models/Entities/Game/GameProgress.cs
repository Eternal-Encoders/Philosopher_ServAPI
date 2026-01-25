using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    [Table("game_progresses")]
    public class GameProgress : IAggregateRoot
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("level_id")]
        public required Guid LevelId { get; set; }

        [Required]
        [Column("last_card_id")]
        public required Guid LastCardId { get; set; }

        [Column("level_ending_id")]
        public Guid? LevelEndingId { get; set; }

        [Column("game_ended")]
        public bool GameEnded { get; set; } = false;

        [Column("human")]
        public int Humanity { get; set; } = 50;

        [Column("robot")]
        public int Robotification { get; set; } = 50;

        [Column("step_number")]
        public int StepNumber { get; set; } = 1;


        [ForeignKey("LevelId")]
        public Level? Level { get; set; }

        [ForeignKey("LevelEndingId")]
        public LevelEnding? LevelEnding { get; set; }

        [ForeignKey("LastCardId")]
        public Card? LastCard { get; set; }
    }
}
