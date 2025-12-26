using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Philosopher_ServAPI.Core.Models.Entities
{
    [Table("users")]
    public class User: IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;


        //Если нужно будет вести историю игр
        //public Guid? ActualGameHistoryId { get; set; }

        //[ForeignKey(nameof(ActualGameHistoryId))]
        //public GameHistory? ActualGameHistory { get; set; }

        public ICollection<GameProgress> GameHistories { get; set; } = [];
    }
}
