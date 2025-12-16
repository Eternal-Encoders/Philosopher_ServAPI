using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Philosopher_ServAPI.Core.Models.Entities.Game;

namespace Philosopher_ServAPI.Infrastructure.Configurations
{
    public class GameHistoryConfiguration : IEntityTypeConfiguration<LevelProgress>
    {
        public void Configure(EntityTypeBuilder<LevelProgress> builder)
        {
            //builder.HasKey(h => h.Id);
        }
    }
}
