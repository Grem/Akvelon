using System.Threading.Tasks;
using Akvelon.TokenService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon.TokenService.Web.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IProcessingRequestService _requestService;

        public HomeController(IProcessingRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> Index(string token, string callback, string ph)
        {
            var (ip, userAgent) = GetDataFromRequest(Request.HttpContext);
            var callbackUrl = await _requestService.ProcessingRequest(ip, userAgent, token, callback, ph);

            if (string.IsNullOrEmpty(callbackUrl)) return Ok();
            
            return Redirect(callbackUrl);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static (string, string) GetDataFromRequest(HttpContext httpContext)
        {
            var ip = httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            
            return (ip, userAgent);
        }
    }
}