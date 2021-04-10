using System.Threading.Tasks;
using Akvelon.TokenService.Services.Interfaces;

namespace Akvelon.TokenService.Services.Services
{
    public class ProcessingRequestService : IProcessingRequestService
    {
        #region Public

        public Task<string> ProcessingRequest(string token, string callback, string ph)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        
        #region Private
        
        #endregion
    }
}