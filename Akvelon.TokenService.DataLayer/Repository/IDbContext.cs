using Akvelon.TokenService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TokenService.DataLayer.Repository
{
    public interface IDbContext
    {
        /// <summary>
        /// Clicks table
        /// </summary>
        DbSet<Click> Clicks { get; set; }
    }
}