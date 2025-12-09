using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Philosopher_ServAPI.Core.Models.Entities;

namespace Philosopher_ServAPI.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasKey(u => u.Id);

            //builder.HasMany(u => u.GameHistories)
            //    .WithOne(h => h.User)
            //    .HasForeignKey(h => h.UserId);

            //builder.HasOne(u => u.ActualGameHistory)
            //    .WithOne()
            //    .HasForeignKey<User>(p => p.ActualGameHistoryId)
            //    .IsRequired(false);
        }
    }
}
