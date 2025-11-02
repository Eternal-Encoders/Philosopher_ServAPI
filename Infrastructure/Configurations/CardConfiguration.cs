using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Philosopher_ServAPI.Core.Models.Entities.Game;

namespace Philosopher_ServAPI.Infrastructure.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.GameHistories)
                .WithOne(h => h.Card)
                .HasForeignKey(h => h.CardId);
        }
    }
}
