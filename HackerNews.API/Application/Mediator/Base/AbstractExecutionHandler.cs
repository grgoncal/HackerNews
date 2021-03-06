using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Infraestructure.Tools;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Mediator.Base
{
    public abstract class AbstractExecutionHandler<T> : AbstractHandler, IRequestHandler<T, Response> where T : IRequest<Response>
    {
        protected readonly ILogger _logger;

        protected AbstractExecutionHandler(ILogger logger)
        {
            _logger = logger;
        }

        internal abstract Task<Response> Execute(T request, CancellationToken cancellationToken);

        public async Task<Response> Handle(T request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var result = await Execute(request, cancellationToken);
                ParseResult(response, result);
            }
            catch (Exception ex)
            {
                _logger.Error($"[{this.GetType().Name}] Error while executing command: {ex}");
                // Deal with logs, errors and so on
                // Can also implement other custom error handlers and behaviors
            }

            return response;
        }

        private void ParseResult(Response response, Response result)
        {
            if (result != null && result?.Error == null)
                response.Content = result.Content;
            else if (result != null && result?.Error != null)
                response.Error = result.Error;
        }
    }
}
