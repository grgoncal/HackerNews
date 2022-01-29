using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infraestructure.Services.Base
{
    public abstract class Service<TClient> where TClient : class
    {
        protected TClient Client;

        protected Service(TClient client)
        {
            Client = client;
        }
    }
}
