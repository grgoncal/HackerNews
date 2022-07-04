using System;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Base
{
    public interface IRedisBase<T> 
    {
        Task AddAsync(string key, object value, TimeSpan? expiresIn = null);
        Task<T> GetAsync(string key);
    }
}
