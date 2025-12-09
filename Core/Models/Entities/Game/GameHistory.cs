using Philosopher_ServAPI.Core.Shared;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    public class GameHistory : IAggregateRoot
    {
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }
        public User? User { get; set; }
        public bool GameEnded { get; set; } = false;

        //Если не нужно будет вести историю ходов
        public required Guid CardId { get; set; }
        public Card? Card { get; set; }

        public double Humanity { get; set; } = 0;
        public double Robotification { get; set; } = 0;
        public int StepNumber { get; set; } = 1;

        //Если нужно будет вести историю ходов
        //public required Guid ActualGameStep { get; set; }
    }
}
