using HackerNews.Domain.Interfaces.App.Services.Cache;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Controllers
{
    [Route("api/[controller]")]
    public class HackerNewsController : Controller
    {
        private readonly INewsCacheService _newsCacheService;

        public HackerNewsController(INewsCacheService newsCacheService)
        {
            _newsCacheService = newsCacheService;
        }

        [HttpGet("best20")]
        public async Task<IActionResult> GetTop20News()
        {
            var response = await _newsCacheService.GetTop20NewsAsync();

            if (response.HasError())
                return StatusCode(500, response);

            return Ok(response.Content);
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
