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
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\CryptoNews.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "CryptoNews", feeds) ?? new();
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("CryptoNews", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("premnews")]
        public async Task<IActionResult> GetPremierLeagueNews()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\PremNews.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "PremNews", feeds) ?? new();
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("PremNews", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("f1news")]
        public async Task<IActionResult> GetF1News()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\F1News.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "Formula 1", feeds);
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("F1News", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("wecnews")]
        public async Task<IActionResult> GetWECNews()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\WecNews.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "WEC", feeds);
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("WEC", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("motogpnews")]
        public async Task<IActionResult> GetMotoGPNews()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\MotoGPNews.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "MotoGP", feeds);
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("MotoGP", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("uknews")]
        public async Task<IActionResult> GetUKNews()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\UKNews.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "UKNews", feeds);
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("UKNews", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("fashionnews")]
        public async Task<IActionResult> GetFashionNews()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\FashionNews.txt");

                var result = await RSSReader.ReadRSSFeeds(currentDirectory, "FashionNews", feeds);
                result = result?.OrderByDescending(o => o.PubDate)
                    .ThenByDescending(t => t.Description != string.Empty)
                    .AsEnumerable().ToList() ?? new();

                JsonToFileHelper.WriteJsonToFile("FashionNews", result, "CachedJsonDataExtended");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
