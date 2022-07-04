using HackerNews.Domain.Constants;
using HackerNews.Domain.Entities.Base;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using HackerNews.Infraestructure.Services.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infraestructure.Services.HackerNews
{
    public class HackerNewsService : Service<IHackerNewsClient>, IHackerNewsService
    {
        private readonly Endpoint _endpoint;

        public HackerNewsService(IHackerNewsClient hackerNewsClient,
            IOptions<AppSettings> settings) : base(hackerNewsClient)
        {
            _endpoint = settings.Value.Endpoints.FirstOrDefault(e => e.Reference == Endpoints.HackerNews);
        }

        public async Task<List<long>> GetIdListOfBestHistoriesAsync()
        {
            var method = _endpoint.Methods[0];
            return await Client.Get<List<long>>(method);
        }

        public async Task<New> GetNewDetailAsync(string newId)
        {
            var method = _endpoint.Methods[1].Replace("{ID}", newId);
            return await Client.Get<New>(method);
        }
    }
}
