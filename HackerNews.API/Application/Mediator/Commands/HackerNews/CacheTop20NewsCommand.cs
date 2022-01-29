using HackerNews.Domain.Entities.Integration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Mediator.Commands.HackerNews
{
    public class CacheTop20NewsCommand : IRequest<Response>
    {
    }
}
