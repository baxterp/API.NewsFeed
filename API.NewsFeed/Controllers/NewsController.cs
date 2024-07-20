using API.NewsFeed.Helpers;
using API.NewsFeed.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        [Route("f1news")]
        public IEnumerable<Item> GetF1News()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\F1News.txt");

            List<Item>? result = null;
            Task.Run(async () =>
            {
                result = await RSSReader.ReadRSSFeeds(currentDirectory, "Formula 1", feeds);
            }).Wait();
            return result ?? new List<Item>();
        }

        [HttpGet]
        [Route("wecnews")]
        public IEnumerable<Item> GetWECNews()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\WecNews.txt");

            List<Item>? result = null;
            Task.Run(async () =>
            {
                result = await RSSReader.ReadRSSFeeds(currentDirectory, "WEC", feeds);
            }).Wait();
            return result ?? new List<Item>();
        }

        [HttpGet]
        [Route("motogpnews")]
        public IEnumerable<Item> GetMotoGPNews()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> feeds = System.IO.File.ReadAllLines(currentDirectory + @"\Feeds\MotoGPNews.txt");

            List<Item>? result = null;
            Task.Run(async () =>
            {
                result = await RSSReader.ReadRSSFeeds(currentDirectory, "MotoGP", feeds);
            }).Wait();
            return result ?? new List<Item>();
        }


        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing()
        {
            return Ok();
        }

    }
}
