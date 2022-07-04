using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.Base;
using HackerNews.Infraestructure.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infraestructure.Services.Base
{
    public abstract class RESTClient : AbstractHandler, IRESTClient
    {
        private readonly ILogger _logger;
        protected abstract string ServiceUrl { get; }

        protected RESTClient(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResult> Get<TResult>(string method) 
            where TResult : class
        {
            var result = default(TResult);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceUrl);

                var response = await client.GetAsync(method, HttpCompletionOption.ResponseContentRead);

                if (response.IsSuccessStatusCode)
                    result = await ParseResultAsync<TResult>(response);
            }

            return result;
        }

        private async Task<TResult> ParseResultAsync<TResult>(HttpResponseMessage response) where TResult : class
        {
            return await DoWorkAsync(async () =>
            {
                var serializedContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(serializedContent);
            }, (e) =>
            {
                _logger.Error($"Error while deserializing get response: {e}");
                throw e.InnerException ?? e;
            });
        }
    }
}
