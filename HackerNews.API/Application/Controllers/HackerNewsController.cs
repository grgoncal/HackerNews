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

        [HttpGet("best20")]
        public IActionResult GetTop20News()
        {
            var getTop20NewsCommand = new GetTop20NewsCommand();
            var response = _mediator.Send(getTop20NewsCommand).Result;

            if (!string.IsNullOrEmpty(response.Error))
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
