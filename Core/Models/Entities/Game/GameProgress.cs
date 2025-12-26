using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    public class GameProgress : IAggregateRoot
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("level_id")]
        public required Guid LevelId { get; set; }

        [ForeignKey("level_id")]
        public Level? Level { get; set; }

        [Column("game_ended")]
        public bool GameEnded { get; set; } = false;

        [Column("level_ending_id")]
        public Guid? LevelEndingId { get; set; }

        [ForeignKey("level_ending_id")]
        public LevelEnding? LevelEnding { get; set; }

        [Column("last_card_id")]
        public required Guid CardId { get; set; }

        [ForeignKey("last_card_id")]
        public Card? Card { get; set; }

        [Column("human")]
        public int Humanity { get; set; } = 50;

        [Column("robot")]
        public int Robotification { get; set; } = 50;

        [Column("step_number")]
        public int StepNumber { get; set; } = 1;



        //public Guid UserId { get; set; }
        //public User? User { get; set; }
    }
}
