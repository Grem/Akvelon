using Akvelon.TokenService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TokenService.DataLayer.Repository
{
    public class TokenDbContext : DbContext
    {
        public TokenDbContext()
        {
        }

        public TokenDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=AkvelonTest;Trusted_Connection=True");
            }
        }

        public virtual DbSet<Click> Clicks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Click>(entity =>
            {
                entity.ToTable("Clicks");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Click");
            });
        }
    }
}