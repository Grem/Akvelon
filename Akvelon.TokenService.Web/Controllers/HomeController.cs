using System.Threading.Tasks;
using Akvelon.TokenService.Services.Interfaces;
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
            var callbackUrl = await _requestService.ProcessingRequest(token, callback, ph);

            if (string.IsNullOrEmpty(callbackUrl)) return Ok();
            
            return Redirect(callbackUrl);
        }
    }
}