using API.NewsFeed.Models;
using API.NewsFeed.Helpers;
using System.Threading;
using System.Collections.Concurrent;

namespace API.NewsFeed.Helpers
{
    public static class RSSReader
    {
        private static readonly HttpClient client = new HttpClient();

        public async static Task<List<Item>> ReadRSSFeeds(string currentDirectory, string category, IEnumerable<string> feeds)
        {
            var concurrentItems = new ConcurrentBag<Item>();

            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            await Parallel.ForEachAsync(feeds, new ParallelOptions { CancellationToken = cancellationToken }, async (feed, token) =>
            {
                var response = await client.GetAsync(feed, token);
                if (response.IsSuccessStatusCode)
                {
                    string xmlData = await response.Content.ReadAsStringAsync();
                    XMLRssFeed rss = XMLParser.ParseXml(xmlData);
                    RssFeed rssFeed = await XMLtoJSONMapper.MapXMLtoJSON(rss, category);

                    Parallel.ForEach(rssFeed.Channel.Items, new ParallelOptions { CancellationToken = token }, item =>
                    {
                        token.ThrowIfCancellationRequested();
                        concurrentItems.Add(new Item
                        {
                            Title = item.Title ?? string.Empty,
                            Link = item.Link ?? string.Empty,
                            Description = item.Description ?? string.Empty,
                            Category = item.Category ?? category,
                            PubDate = item.PubDate ?? string.Empty,
                            ImageURL = item.ImageURL ?? string.Empty,
                        });
                    });
                }
            });
            return concurrentItems.ToList();
        }
    }
}

