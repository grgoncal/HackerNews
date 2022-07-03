using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infraestructure.Tools
{
    public abstract class AbstractHandler
    {
        public void RetryDoWork(Action action, Action<Exception> errorAction, int retryCount)
        {
            try
            {
                var retryPolicy = Policy.Handle<Exception>().Retry(retryCount, (e, retryCount) =>
                {
                    errorAction(e);
                });

                retryPolicy.Execute(() =>
                {
                    action();
                });
            }
            catch (Exception e)
            {
                errorAction(e);
            }
        }

        public async Task RetryDoWorkAsync(Func<Task> action, Action<Exception> errorAction, int retryCount)
        {
            try
            {
                var retryPolicy = Policy.Handle<Exception>().RetryAsync(retryCount, (e, retryCount) =>
                {
                    errorAction(e);
                });

                await retryPolicy.ExecuteAsync(async () =>
                {
                    await action();
                });
            }
            catch (Exception e)
            {
                errorAction(e);
            }
        }

        public void DoWork(Action action, Action<Exception> errorAction)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                errorAction(e);
            }
        }

        public async Task DoWorkAsync(Func<Task> action, Action<Exception> errorAction)
        {
            try
            {
                await action();
            }
            catch (Exception e)
            {
                errorAction(e);
            }
        }

        public async Task<T> DoWorkAsync<T>(Func<Task<T>> action, Action<Exception> errorAction)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                errorAction(e);
                throw;
            }
        }
    }
}
