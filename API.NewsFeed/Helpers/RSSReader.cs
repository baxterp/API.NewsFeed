using API.NewsFeed.Models;
using API.NewsFeed.Helpers;

namespace API.NewsFeed.Helpers
{
    public static class RSSReader
    {
        public async static Task<List<Item>> ReadRSSFeeds(string currentDirectory, string category, IEnumerable<string> feeds)
        {
            using (var client = new HttpClient())
            {
                List<Item> items = new List<Item>();
                foreach (var feed in feeds)
                {
                    var response = await client.GetAsync(feed);
                    if (response.IsSuccessStatusCode)
                    {
                        string xmlData = await response.Content.ReadAsStringAsync();
                        XMLRssFeed rss = XMLParser.ParseXml(xmlData);
                        RssFeed rssFeed = XMLtoJSONMapper.MapXMLtoJSON(rss, category);
                        items.AddRange(rssFeed.Channel.Items);
                    }
                }
                return items;
            }
        }
    }
}
