using Philosopher_ServAPI.Core.Models.Entities.Game;

namespace Philosopher_ServAPI.Core.Models.DTOs
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public Guid? ActualGameHistoryId { get; set; }
    }
}
