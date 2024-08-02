using API.NewsFeed.Helpers;
using API.NewsFeed.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        [Route("f1news")]
        public async Task<IActionResult> GetF1News()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\F1News.txt");

            List<Item> result = await RSSReader.ReadRSSFeeds(currentDirectory, "Formula 1", feeds);

            var orderedResult = result.OrderBy(o => o.PubDate).AsEnumerable();
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "F1News", orderedResult } });
        }

        [HttpGet]
        [Route("wecnews")]
        public async Task<IActionResult> GetWECNews()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\WecNews.txt");

            List<Item> result = await RSSReader.ReadRSSFeeds(currentDirectory, "WEC", feeds);

            var orderedResult = result.OrderBy(o => o.PubDate).AsEnumerable();
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "WECNews", orderedResult } });
        }

        [HttpGet]
        [Route("motogpnews")]
        public async Task<IActionResult> GetMotoGPNews()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\MotoGPNews.txt");

            List<Item> result = await RSSReader.ReadRSSFeeds(currentDirectory, "MotoGP", feeds);

            var orderedResult = result.OrderBy(o => o.PubDate).AsEnumerable();
            return Ok(new Dictionary<string, IEnumerable<Item>> { { "MotoGPNews", orderedResult } });
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
