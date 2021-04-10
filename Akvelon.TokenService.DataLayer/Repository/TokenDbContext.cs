using Akvelon.TokenService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TokenService.DataLayer.Repository
{
    public class TokenDbContext : DbContext
    {
        public DbSet<Click> Clicks { get; set; }
        
        public TokenDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}