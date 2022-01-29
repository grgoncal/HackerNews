using HackerNews.Domain.Interfaces.Infra.Logger;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infraestructure.Tools.SafeCaller
{
    public static class SafeCaller 
    {
        public static void SafeCall(Action action, ILogger logger)
        {
            var retryPolicy = Policy.Handle<Exception>().Retry(3, (e, retryCount) =>
            {
                logger.Error($"Retry {retryCount} returned error {e}");
            });

            retryPolicy.Execute(() =>
            {
                action();
            });
        }
    }
}
