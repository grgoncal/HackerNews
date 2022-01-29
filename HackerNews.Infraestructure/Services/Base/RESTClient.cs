using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infraestructure.Services.Base
{
    public abstract class RESTClient : IRESTClient
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
                client.BaseAddress = new Uri($"{ServiceUrl}");

                var response = await client.GetAsync(method, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    result = GetResult<TResult>(response);
                }
            }

            return result;
        }

        private TResult GetResult<TResult>(HttpResponseMessage response) where TResult : class
        {
            try
            {
                var serializedContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonConvert.DeserializeObject<TResult>(serializedContent);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while deserializing get response: {ex}");
                throw;
            }
        }
    }
}
