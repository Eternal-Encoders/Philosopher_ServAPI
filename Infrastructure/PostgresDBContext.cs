using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Infrastructure.Configurations;

namespace Philosopher_ServAPI.Infrastructure
{
    public class PostgresDBContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }
        //Если нужно будет вести историю ходов
        //public DbSet<GameStep> GameSteps { get; set; }

        public PostgresDBContext(DbContextOptions<PostgresDBContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureDeleted(); //Для пересоздания БД
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new GameHistoryConfiguration());
            //modelBuilder.ApplyConfiguration(new CardConfiguration());

            //modelBuilder.Entity<Card>().HasData(
            //    new Card
            //    {
            //        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            //        HumanityDelta1 = -1,
            //        HumanityDelta2 = 1,
            //        RobotificationDelta1 = 1,
            //        RobotificationDelta2 = -1,
            //    }
            //);
        }
    }
}
