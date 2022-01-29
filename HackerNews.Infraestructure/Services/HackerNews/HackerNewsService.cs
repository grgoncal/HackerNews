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
        private readonly IOptions<AppSettings> _settings;

        public HackerNewsService(IHackerNewsClient hackerNewsClient,
            IOptions<AppSettings> settings) : base(hackerNewsClient)
        {
            _settings = settings;
        }

        public List<long> GetListOfBestHistoriesIds()
        {
            var method = _settings.Value.Endpoints.FirstOrDefault(e => e.Reference == Endpoints.HackerNews).Methods[0];
            return Client.Get<List<long>>(method).GetAwaiter().GetResult();
        }

        public New GetNewDetails(long newId)
        {
            var method = _settings.Value.Endpoints.FirstOrDefault(e => e.Reference == Endpoints.HackerNews).Methods[1];
            var id = newId.ToString();

            return Client.Get<New>(method.Replace("{ID}", id)).GetAwaiter().GetResult();
        }
    }
}
