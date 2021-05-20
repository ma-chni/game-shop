using GameShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Data
{
    class GameShopContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<ArticleOrder> ArticleOrder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=GameShop;User=sa;Password=Doglover420;");
            #endregion
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleOrder>()
               .HasKey(bc => new { bc.ArticleId, bc.OrderId });

            modelBuilder.Entity<ArticleOrder>()
               .HasOne(bc => bc.Article)
               .WithMany(c => c.Orders)
               .HasForeignKey(bc => bc.ArticleId);

            modelBuilder.Entity<ArticleOrder>()
               .HasOne(bc => bc.Order)
               .WithMany(c => c.Articles)
               .HasForeignKey(bc => bc.OrderId);
        }
    }
}
