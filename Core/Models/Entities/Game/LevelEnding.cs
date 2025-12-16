using Philosopher_ServAPI.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace Philosopher_ServAPI.Core.Models.Entities.Game
{
    public class LevelEnding : IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; } 

        public string Description { get; set; } = string.Empty;
    }
}
