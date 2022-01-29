using HackerNews.Domain.Entities.Integration;
using MediatR;

namespace HackerNews.API.Application.Mediator.Commands.HackerNews
{
    public class GetTop20NewsCommand : IRequest<Response>
    {
    }
}
