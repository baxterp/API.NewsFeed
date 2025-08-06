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
                using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                timeoutCts.CancelAfter(600);

                try
                {
                    var response = await client.GetAsync(feed, timeoutCts.Token);
                    if (response.IsSuccessStatusCode)
                    {
                        string xmlData = await response.Content.ReadAsStringAsync(timeoutCts.Token);
                        XMLRssFeed rss = XMLParser.ParseXml(xmlData);
                        RssFeed rssFeed = await XMLtoJSONMapper.MapXMLtoJSON(rss, category);

                        Parallel.ForEach(rssFeed.Channel.Items, new ParallelOptions { CancellationToken = token }, item =>
                        {
                            token.ThrowIfCancellationRequested();
                            DateTime pubDate = DateTime.Now;
                            DateTime.TryParse(item.PubDate.ToString("yyyy MM dd"), out pubDate);
                            if (pubDate > DateTime.Now - new TimeSpan(28, 0, 0, 0))
                            {
                                concurrentItems.Add(new Item
                                {
                                    Title = item.Title ?? string.Empty,
                                    Link = item.Link ?? string.Empty,
                                    Description = item.Description ?? string.Empty,
                                    Category = item.Category ?? category,
                                    PubDate = pubDate,
                                    ImageURL = item.ImageURL ?? string.Empty,
                                });
                            }
                        });
                    }
                }
                catch (OperationCanceledException ex)
                {
                    // Timeout or cancellation occurred, skip this feed
                }
            });
            return concurrentItems.ToList();
        }
    }
}