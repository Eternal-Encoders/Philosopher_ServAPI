using Philosopher_ServAPI.Core.Shared;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    public class Card: IAggregateRoot
    {
        public required Guid Id { get; set; }
        public required double HumanityDelta1 { get; set; }
        public required double HumanityDelta2 { get; set; }
        public required double RobotificationDelta1 { get; set; }
        public required double RobotificationDelta2 { get; set; }
        public string Text { get; set; } = String.Empty;
        public string Choice1Text { get; set; } = String.Empty;
        public string Choice2Text { get; set; } = String.Empty;
        public string ImageLink { get; set; } = String.Empty;

        public ICollection<GameHistory> GameHistories { get; set; } = [];
    }
}
