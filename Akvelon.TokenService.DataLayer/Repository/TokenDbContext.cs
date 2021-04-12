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

        public virtual DbSet<Callback> Callbacks { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Requests");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Request");
            });
            
            modelBuilder.Entity<Callback>(entity =>
            {
                entity.ToTable("Callbacks");

                entity.HasIndex(e => e.Id)
                    .HasDatabaseName("PK_CallBack")
                    .IsUnique();

                entity.HasOne(d => d.Request)
                    .WithOne(p => p.Callback)
                    .HasForeignKey<Callback>(d => d.Id)
                    .HasConstraintName("FK_Request_CallBack");
            });
        }
    }
}