using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    public class LevelEnding : IAggregateRoot
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("level_id")]
        public required Guid LevelId { get; set; }

        [ForeignKey("level_id")]
        public Level? Level { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("image_link")]
        public string ImageLink { get; set; } = string.Empty;

        [Column("robot_condition")]
        public int? RobotCondition { get; set; }

        [Column("human_condition")]
        public int? HumanCondition { get; set; }
    }
}
