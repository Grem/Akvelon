using System;
using System.Net.Http;
using System.Threading.Tasks;
using Akvelon.TokenService.Services.Interfaces;

namespace Akvelon.TokenService.Services.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;

        public HttpService()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
                CheckCertificateRevocationList = false
            };
            _client = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(3),
            };
        }
        
        public async Task<string> Get(string url)
        {
            string result;
            try
            {
                var response = await _client.GetAsync(url);
                result = response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                throw new Exception("GET URL " + url + "\r\n\tError message: " + e.Message);
            }

            return result;
        }
    }
}