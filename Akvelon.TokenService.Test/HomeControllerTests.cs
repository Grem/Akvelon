using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Akvelon.TokenService.Web;
using Xunit;
using FluentAssertions;

namespace Akvelon.TokenService.Test
{
    public class HomeController : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly string url = "http://localhost:5000/abctoken?callback=http://google.com/?q={ph}&ph=akvelon";
        
        public HomeController(TestWebApplicationFactory<Startup> factory)
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
                CheckCertificateRevocationList = false
            };
            _httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(3),
            };
        }

        [Fact]
        public async Task RequestWithToken()
        {
            var response = await _httpClient.GetAsync(url);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}