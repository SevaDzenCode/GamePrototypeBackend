using GamePrototypeBackend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GamePrototypeBackend.Data.EF
{
    public class GamePrototypeDbContext : DbContext
    {
        public GamePrototypeDbContext(DbContextOptions<GamePrototypeDbContext> options) : base(options) { }

        public DbSet<Balance> Balances { get; set; } = null!;
        public DbSet<Coins> Coins { get; set; } = null!;
        public DbSet<CoinsTransfer> CoinsTransfers { get; set; } = null!;
        public DbSet<Deposit> Deposits { get; set; } = null!;
        public DbSet<Exchanger> Exchangers { get; set; } = null!;
        public DbSet<Refferal> Refferals { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Settings> Settings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Withdrawal> Withdrawals { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role user = new Role() { Id = 1, role = "User" };
            Role admin = new Role() { Id = 2, role = "Admin" };

            modelBuilder.Entity<Role>().HasData(new Role[] { admin, user });
            base.OnModelCreating(modelBuilder);
        }
    }

    //public class GamePrototypeDbContextFactory : IDesignTimeDbContextFactory<GamePrototypeDbContext>
    //{
    //    public GamePrototypeDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<GamePrototypeDbContext>();
    //        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GamePrototype;Trusted_Connection=True");

    //        return new GamePrototypeDbContext(optionsBuilder.Options);
    //    }
    //}
}
