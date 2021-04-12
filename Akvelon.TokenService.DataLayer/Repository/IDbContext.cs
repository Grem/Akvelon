using Akvelon.TokenService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TokenService.DataLayer.Repository
{
    public interface IDbContext
    {
        /// <summary>
        /// Requests table
        /// </summary>
        DbSet<Request> Clicks { get; set; }
    }
}