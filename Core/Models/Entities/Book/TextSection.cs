using Microsoft.Extensions.Primitives;
using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Book
{
    [Table("text_sections")]
    public class TextSection : IAggregateRoot
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("title")]
        public required string Title { get; set; }

        [Column("text")]
        public string Text { get; set; } = String.Empty;

        [Column("type")]
        public string Type { get; set; } = String.Empty;

        [ForeignKey(nameof(Type))]
        public TextSectionType? SectionType { get; set; }
    }
}
