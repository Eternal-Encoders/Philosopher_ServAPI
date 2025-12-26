using Philosopher_ServAPI.Core.Models.Entities.Book;
using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    [Table("level")]
    public class Level : IAggregateRoot
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("name")]
        public string Name { get; set; } = String.Empty;

        [Column("description")]
        public string Description { get; set; } = String.Empty;

        [Column("text_section_id")]
        public Guid TextSectionId { get; set; }

        [ForeignKey("text_section_id")]
        public TextSection? TextSection { get; set; }

        public ICollection<Card> Cards { get; set; } = [];

        public ICollection<LevelEnding> LevelEndings { get; set; } = [];
    }
}
