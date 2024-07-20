using System.Xml.Serialization;

namespace API.NewsFeed.Models
{
    public class RssFeed
    {
        public Channel Channel { get; set; }
    }

    public class Channel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PubDate { get; set; }
        public string ImageURL { get; set; }
    }

    public class Enclosure
    {
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
