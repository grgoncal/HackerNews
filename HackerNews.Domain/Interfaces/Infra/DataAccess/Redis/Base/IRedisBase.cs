using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Base
{
    public interface IRedisBase<T> 
    {
        void Add(string key, object value, TimeSpan? expiresIn = null);
        T Get(string key);
    }
}
