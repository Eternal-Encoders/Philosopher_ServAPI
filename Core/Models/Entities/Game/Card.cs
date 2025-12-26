using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    [Table("card")]
    public class Card: IAggregateRoot
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("level_id")]
        public Guid LevelId { get; set; }

        [ForeignKey("level_id")]
        public Level? Level { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("text")]
        public string Text { get; set; } = string.Empty;

        [Column("first_choice_text")]
        public string FirstChoiceText { get; set; } = string.Empty;

        [Column("second_choice_text")]
        public string SecondChoiceText { get; set; } = string.Empty;

        [Column("first_human_delta")]
        public int HumanDelta1 { get; set; } = 0;

        [Column("second_human_delta")]
        public int HumanDelta2 { get; set; } = 0;

        [Column("first_robot_delta")]
        public int RobotDelta1 { get; set; } = 0;

        [Column("second_robot_delta")]
        public int RobotDelta2 { get; set; } = 0;

        [Column("image_link")]
        public string ImageLink { get; set; } = string.Empty;
    }
}
