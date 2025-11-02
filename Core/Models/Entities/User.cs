using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Shared;

namespace Philosopher_ServAPI.Core.Models.Entities
{
    public class User: IAggregateRoot
    {
        public required Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

        //Если нужно будет вести историю игр
        public Guid? ActualGameHistoryId { get; set; }
        public GameHistory? ActualGameHistory { get; set; }

        public ICollection<GameHistory> GameHistories { get; set; } = [];
    }
}
