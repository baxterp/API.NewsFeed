using API.NewsFeed.Helpers;
using API.NewsFeed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachingNewsController : ControllerBase
    {
        [HttpGet]
        [Route("cryptonews")]
        public async Task<IActionResult> GetCryptoNews()
        {
            var result = JsonToFileHelper.ReadJsonFromFileForCach("CryptoNews");
            if (result == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\CryptoNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "CryptoNews", feeds) ?? new();
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("CryptoNews", result, "CachedJsonDataExtended");
            }
            return Ok();
        }

        [HttpGet]
        [Route("f1news")]
        public async Task<IActionResult> GetF1News()
        {
            var result = JsonToFileHelper.ReadJsonFromFileForCach("F1News");
            if (result == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\F1News.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "Formula 1", feeds);

                JsonToFileHelper.WriteJsonToFile("F1News", result, "CachedJsonDataExtended");
            }
            return Ok();
        }

        [HttpGet]
        [Route("wecnews")]
        public async Task<IActionResult> GetWECNews()
        {
            var result = JsonToFileHelper.ReadJsonFromFileForCach("WEC");
            if (result == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\WecNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "WEC", feeds);

                JsonToFileHelper.WriteJsonToFile("WEC", result, "CachedJsonDataExtended");
            }
            return Ok();
        }

        [HttpGet]
        [Route("motogpnews")]
        public async Task<IActionResult> GetMotoGPNews()
        {
            var result = JsonToFileHelper.ReadJsonFromFileForCach("MotoGP");
            if (result == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\MotoGPNews.txt");

                result = await RSSReader.ReadRSSFeeds(currentDirectory, "MotoGP", feeds);

                JsonToFileHelper.WriteJsonToFile("MotoGP", result, "CachedJsonDataExtended");
            }
            return Ok();
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
