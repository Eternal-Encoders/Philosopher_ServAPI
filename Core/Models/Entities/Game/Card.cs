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
        public string Name { get; set; } = String.Empty;

        [Column("text")]
        public string Text { get; set; } = String.Empty;

        [Column("first_choice_text")]
        public string FirstChoiceText { get; set; } = String.Empty;

        [Column("second_choice_text")]
        public string SecondChoiceText { get; set; } = String.Empty;

        [Column("first_hum_delta")]
        public required double HumanityDelta1 { get; set; }

        [Column("second_hum_delta")]
        public required double HumanityDelta2 { get; set; }

        [Column("first_rob_delta")]
        public required double RobotificationDelta1 { get; set; }

        [Column("second_rob_delta")]
        public required double RobotificationDelta2 { get; set; }

        [Column("image_link")]
        public string ImageLink { get; set; } = String.Empty;
    }
}
