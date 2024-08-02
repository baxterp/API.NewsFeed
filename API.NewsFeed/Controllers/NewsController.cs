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
        [Route("f1news")]
        public async Task<IActionResult> GetF1News()
        {
            string cacheKey = "F1News";
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
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "F1News", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("wecnews")]
        public async Task<IActionResult> GetWECNews()
        {
            string cacheKey = "WECNews";
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
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "WECNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("motogpnews")]
        public async Task<IActionResult> GetMotoGPNews()
        {
            string cacheKey = "MotoGPNews";
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
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "MotoGPNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("fashionnews")]
        public async Task<IActionResult> GetFashionNews()
        {
            string cacheKey = "FashionNews";
            if (!_cache.TryGetValue(cacheKey, out List<Item> result))
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\FashionNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "FashionNews", feeds) ?? new();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                };

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }
            var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "FashionNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("cryptonews")]
        public async Task<IActionResult> GetCryptoNews()
        {
            string cacheKey = "CryptoNews";
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
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "CryptoNews", orderedResult ?? new List<Item>() } });
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
