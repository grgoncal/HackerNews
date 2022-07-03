using HackerNews.Domain.Entities.Integration;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces.App.Services.Cache
{
    public interface INewsCacheService
    {
        Task<Response> GetTop20NewsAsync();
    }
}
