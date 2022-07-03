using HackerNews.Domain.Entities.Integration;
using MediatR;

namespace HackerNews.API.Domain.Entities.Mediator.Commands
{
    public class CacheTop20NewsCommand : IRequest<Response>
    {
    }
}
