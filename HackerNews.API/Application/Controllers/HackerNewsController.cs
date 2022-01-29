using HackerNews.API.Application.Mediator.Commands.HackerNews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.API.Application.Controllers
{
    [Route("api/[controller]")]
    public class HackerNewsController : Controller
    {
        private readonly IMediator _mediator;

        public HackerNewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetTop20News")]
        public IActionResult GetTop20News()
        {
            var getTop20NewsCommand = new GetTop20NewsCommand();
            var response = _mediator.Send(getTop20NewsCommand).Result;

            if (!string.IsNullOrEmpty(response.Error))
                return StatusCode(500, response);

            return Ok(response);
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
