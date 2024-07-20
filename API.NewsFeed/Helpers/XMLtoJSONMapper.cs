using API.NewsFeed.Models;
using AutoMapper;

namespace API.NewsFeed.Helpers
{
    public static class XMLtoJSONMapper
    {
        public static RssFeed MapXMLtoJSON(XMLRssFeed XMLRssFeed, string category)
        {
            RssFeed rssFeed = new RssFeed();
            rssFeed.Channel = new Channel();
            rssFeed.Channel.Title = XMLRssFeed.Channel.Title;
            rssFeed.Channel.Link = XMLRssFeed.Channel.Link;
            rssFeed.Channel.PubDate = XMLRssFeed.Channel.PubDate;

            rssFeed.Channel.Items = new List<Item>();
            foreach (var item in XMLRssFeed.Channel.Items)
            {
                rssFeed.Channel.Items.Add(new Item
                {
                    Title = item.Title ?? string.Empty,
                    Link = item.Link ?? string.Empty,
                    Description = item.Description ?? string.Empty,
                    Category = item.Category ?? category,
                    PubDate = item.PubDate ?? string.Empty,
                    ImageURL = item.Enclosure?.Url ?? string.Empty,
                });
            }
            return rssFeed;
        }
    }
}
