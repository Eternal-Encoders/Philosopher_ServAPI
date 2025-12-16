using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Infrastructure.Configurations;

namespace Philosopher_ServAPI.Infrastructure
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<LevelProgress> GameHistories { get; set; }
        //Если нужно будет вести историю ходов
        //public DbSet<GameStep> GameSteps { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> dbContextOptions,
            IWebHostEnvironment env) : base(dbContextOptions)
        {
            if (env.IsDevelopment())
                Database.EnsureDeleted();

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
