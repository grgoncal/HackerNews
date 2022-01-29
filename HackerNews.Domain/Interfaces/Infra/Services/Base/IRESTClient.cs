using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces.Infra.Services.Base
{
    public interface IRESTClient
    {
        Task<TResult> Get<TResult>(string method) 
            where TResult : class;
    }
}
