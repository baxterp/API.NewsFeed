using API.NewsFeed.Helpers;
using API.NewsFeed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromDays(1);

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        [Route("cryptonews")]
        [Route("cryptonews/{numberOfRecords}/{numberOfDays}")]
        public async Task<IActionResult> GetCryptoNews(int numberOfRecords = 0, int numberOfDays = 0)
        {
            string cacheKey = "CryptoNews" + numberOfRecords + numberOfDays;
            if (!_cache.TryGetValue(cacheKey, out List<Item> result))
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\CryptoNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "CryptoNews", feeds) ?? new();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                };

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }
            var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

            if (numberOfRecords > 0)
                orderedResult = orderedResult?.Take(numberOfRecords);
            else if (numberOfDays > 0)
                orderedResult = orderedResult?
                    .Where(o => o.PubDate > DateTime.Now - new TimeSpan(numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            else
                orderedResult = orderedResult?.Take(10);

            return Ok(new Dictionary<string, IEnumerable<Item>> { { "CryptoNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("f1news")]
        [Route("f1news/{numberOfRecords}/{numberOfDays}")]
        public async Task<IActionResult> GetF1News(int numberOfRecords = 0, int numberOfDays = 0)
        {
            string cacheKey = "F1News" + numberOfRecords + numberOfDays;
            if (!_cache.TryGetValue(cacheKey, out List<Item> result))
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\F1News.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "Formula 1", feeds);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                };

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }
            var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

            if (numberOfRecords > 0)
                orderedResult = orderedResult?.Take(numberOfRecords);
            else if (numberOfDays > 0)
                orderedResult = orderedResult?
                    .Where(o => o.PubDate > DateTime.Now - new TimeSpan(numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            else
                orderedResult = orderedResult?.Take(10);

            return Ok(new Dictionary<string, IEnumerable<Item>> { { "F1News", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("wecnews")]
        [Route("wecnews/{numberOfRecords}/{numberOfDays}")]
        public async Task<IActionResult> GetWECNews(int numberOfRecords = 0, int numberOfDays = 0)
        {
            string cacheKey = "WECNews" + numberOfRecords + numberOfDays;
            if (!_cache.TryGetValue(cacheKey, out List<Item> result))
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\WecNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "WEC", feeds) ?? new();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                };

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }
            var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

            if (numberOfRecords > 0)
                orderedResult = orderedResult?.Take(numberOfRecords);
            else if (numberOfDays > 0)
                orderedResult = orderedResult?
                    .Where(o => o.PubDate > DateTime.Now - new TimeSpan(numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            else
                orderedResult = orderedResult?.Take(10);

            return Ok(new Dictionary<string, IEnumerable<Item>> { { "WECNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("motogpnews")]
        [Route("motogpnews/{numberOfRecords}/{numberOfDays}")]
        public async Task<IActionResult> GetMotoGPNews(int numberOfRecords = 0, int numberOfDays = 0)
        {
            string cacheKey = "MotoGPNews" + numberOfRecords + numberOfDays;
            if (!_cache.TryGetValue(cacheKey, out List<Item> result))
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\MotoGPNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "MotoGP", feeds) ?? new();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                };

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }
            var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

            if (numberOfRecords > 0)
                orderedResult = orderedResult?.Take(numberOfRecords);
            else if (numberOfDays > 0)
                orderedResult = orderedResult?
                    .Where(o => o.PubDate > DateTime.Now - new TimeSpan(numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            else
                orderedResult = orderedResult?.Take(10);

            return Ok(new Dictionary<string, IEnumerable<Item>> { { "MotoGPNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
