using HackerNews.Domain.Entities.Integration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.API.Domain.Entities.Mediator.Commands
{
    public class CacheTop20NewsCommand : IRequest<Response>
    {
    }
}
