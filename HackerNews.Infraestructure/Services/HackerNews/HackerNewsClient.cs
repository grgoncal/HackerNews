using HackerNews.Domain.Constants;
using HackerNews.Domain.Entities.Base;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using HackerNews.Infraestructure.Services.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Infraestructure.Services.HackerNews
{
    public class HackerNewsClient : RESTClient, IHackerNewsClient
    {
        protected readonly IOptions<AppSettings> _settings;

        public HackerNewsClient(IOptions<AppSettings> settings,
            ILogger logger) : base(logger)
        {
            _settings = settings;
        }

        protected override string ServiceUrl => 
            _settings.Value.Endpoints.FirstOrDefault(e => e.Reference == Endpoints.HackerNews).BaseUrl;
    }
}
