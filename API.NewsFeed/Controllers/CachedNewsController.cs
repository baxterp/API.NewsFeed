using API.NewsFeed.Helpers;
using API.NewsFeed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.NewsFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachedNewsController : ControllerBase
    {
        [HttpGet]
        [Route("cryptonews")]
        [Route("cryptonews/{numberOfRecords}/{numberOfDays}")]
        public IActionResult GetCryptoNews(int? numberOfRecords = null, int? numberOfDays = null)
        {
            try
            {
                var result = JsonToFileHelper.ReadJsonFromFileForCach("CryptoNews");
                var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

                if (numberOfRecords > 0)
                    orderedResult = orderedResult?.Take((int)numberOfRecords);
                else if (numberOfDays > 0)
                    orderedResult = orderedResult?
                        .Where(o => o.PubDate > DateTime.Now - new TimeSpan((int)numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                else if (numberOfRecords == null && numberOfDays == null)
                    orderedResult = orderedResult?.Take(10);

                return Ok(new Dictionary<string, IEnumerable<Item>> { { "CryptoNews", orderedResult ?? new List<Item>() } });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("f1news")]
        [Route("f1news/{numberOfRecords}/{numberOfDays}")]
        public IActionResult GetF1News(int? numberOfRecords = null, int? numberOfDays = null)
        {
            try
            {
                var result = JsonToFileHelper.ReadJsonFromFileForCach("F1News");
                var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

                if (numberOfRecords > 0)
                    orderedResult = orderedResult?.Take((int)numberOfRecords);
                else if (numberOfDays > 0)
                    orderedResult = orderedResult?
                        .Where(o => o.PubDate > DateTime.Now - new TimeSpan((int)numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                else if (numberOfRecords == null && numberOfDays == null)
                    orderedResult = orderedResult?.Take(10);

                return Ok(new Dictionary<string, IEnumerable<Item>> { { "F1News", orderedResult ?? new List<Item>() } });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");    
            }
        }

        [HttpGet]
        [Route("wecnews")]
        [Route("wecnews/{numberOfRecords}/{numberOfDays}")]
        public IActionResult GetWECNews(int? numberOfRecords = null, int? numberOfDays = null)
        {
            try
            {
                var result = JsonToFileHelper.ReadJsonFromFileForCach("WEC");
                var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

                if (numberOfRecords > 0)
                    orderedResult = orderedResult?.Take((int)numberOfRecords);
                else if (numberOfDays > 0)
                    orderedResult = orderedResult?
                        .Where(o => o.PubDate > DateTime.Now - new TimeSpan((int)numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                else if (numberOfRecords == null && numberOfDays == null)
                    orderedResult = orderedResult?.Take(10);

                return Ok(new Dictionary<string, IEnumerable<Item>> { { "WECNews", orderedResult ?? new List<Item>() } });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("motogpnews")]
        [Route("motogpnews/{numberOfRecords}/{numberOfDays}")]
        public IActionResult GetMotoGPNews(int? numberOfRecords = null, int? numberOfDays = null)
        {
            try
            {
                var result = JsonToFileHelper.ReadJsonFromFileForCach("MotoGP");
                var orderedResult = result?.OrderByDescending(o => o.PubDate).AsEnumerable();

                if (numberOfRecords > 0)
                    orderedResult = orderedResult?.Take((int)numberOfRecords);
                else if (numberOfDays > 0)
                    orderedResult = orderedResult?
                        .Where(o => o.PubDate > DateTime.Now - new TimeSpan((int)numberOfDays, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                else if (numberOfRecords == null && numberOfDays == null)
                    orderedResult = orderedResult?.Take(10);

                return Ok(new Dictionary<string, IEnumerable<Item>> { { "MotoGPNews", orderedResult ?? new List<Item>() } });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
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
