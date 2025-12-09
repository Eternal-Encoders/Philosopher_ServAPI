using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities.Book
{
    [Table("text_section_types")]
    public class TextSectionType
    {
        [Key]
        [Column("name")]
        public string Name { get; set; }
    }
}
