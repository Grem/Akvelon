using System;
using System.Threading.Tasks;
using Akvelon.TokenService.Core.Models;
using Akvelon.TokenService.DataLayer.Repository;
using Akvelon.TokenService.Services.Interfaces;

namespace Akvelon.TokenService.Services.Services
{
    public class ProcessingRequestService : IProcessingRequestService
    {
        private readonly TokenDbContext _context;

        public ProcessingRequestService(TokenDbContext context)
        {
            _context = context;
        }

        #region Public

        public async Task<string> ProcessingRequest(string ip, string userAgent, string token, string callback,
            string ph)
        {
            Validate(token, callback, ph);

            var click = new Click
            {
                Id = Guid.NewGuid(),
                ClickTime = DateTime.UtcNow,
                Ip = ip,
                UserAgent = userAgent,
                Token = token,
            };

            await _context.Clicks.AddAsync(click);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        #endregion
        
        #region Private
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="callback"></param>
        /// <param name="ph"></param>
        private static void Validate(string token, string callback, string ph)
        {
            if (string.IsNullOrEmpty(token)) 
                throw new ArgumentNullException("No token specified!");

            var isPhExist = callback.IndexOf("{ph}", StringComparison.Ordinal) >= 0;
            
            if (isPhExist && string.IsNullOrEmpty(ph))
                throw new ArgumentNullException("No ph specified!");
        }

        #endregion
    }
}