using API.NewsFeed.Models;
using AutoMapper;
using System.Collections.Concurrent;
using System.Threading;

namespace API.NewsFeed.Helpers
{
    public static class XMLtoJSONMapper
    {
        public static async Task<RssFeed> MapXMLtoJSON(XMLRssFeed XMLRssFeed, string category)
        {
            RssFeed rssFeed = new RssFeed();
            rssFeed.Channel = new Channel();
            rssFeed.Channel.Title = XMLRssFeed.Channel.Title;
            rssFeed.Channel.Link = XMLRssFeed.Channel.Link;
            rssFeed.Channel.PubDate = XMLRssFeed.Channel.PubDate;
            var items = new ConcurrentBag<Item>();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            await Parallel.ForEachAsync(XMLRssFeed.Channel.Items, new ParallelOptions { CancellationToken = token }, (item, ct) =>
            {
                ct.ThrowIfCancellationRequested();
                DateTime pubDate = DateTime.Now;
                DateTime.TryParse(item.PubDate.ToString(), out pubDate);
                items.Add(new Item
                {
                    Title = item.Title ?? string.Empty,
                    Link = item.Link ?? string.Empty,
                    Description = item.Description ?? string.Empty,
                    Category = item.Category ?? category,
                    PubDate = pubDate,
                    ImageURL = item.Enclosure?.Url ?? string.Empty,
                });
                return ValueTask.CompletedTask; // Ensure the delegate is synchronous
            });

            rssFeed.Channel.Items = items.ToList();
            return rssFeed;
        }
    }
}
