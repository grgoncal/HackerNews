using HackerNews.Domain.Entities.Base;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Tests.UnitTesting.HackerNews.REST
{
    public class HackerNewsRestTest
    {
        private IHackerNewsService _hackerNewsService;

        [SetUp]
        public void Setup()
        {
            _hackerNewsService = IoC.GetHackerNewsRestClient();
        }

        [Test]
        public async Task When_GetAllStoriesIds_Expect_AllStoriesIds()
        {
            var result = await _hackerNewsService.GetIdListOfBestHistoriesAsync();

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count > 1);
        }

        [Test]
        public async Task When_GetStoryDetail_Expect_StoryDetail()
        {
            var idList = await _hackerNewsService.GetIdListOfBestHistoriesAsync();
            var id = idList.FirstOrDefault();

            var detailedNew = await _hackerNewsService.GetNewDetailAsync(id.ToString());

            Assert.IsTrue(detailedNew != null);
            Assert.IsTrue(!string.IsNullOrEmpty(detailedNew.Title));
            Assert.IsTrue(!string.IsNullOrEmpty(detailedNew.By));
            Assert.IsTrue(detailedNew.Id == id);
            Assert.IsTrue(detailedNew.Time > 0);
        }

    }
}
