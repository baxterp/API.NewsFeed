using System.Xml.Serialization;

namespace API.NewsFeed.Models
{
    [XmlRoot("rss")]
    public class XMLRssFeed
    {
        [XmlElement("channel")]
        public XMLChannel Channel { get; set; }
    }

    public class XMLChannel
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("item")]
        public List<XMLItem> Items { get; set; }
    }

    public class XMLItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("category")]
        public string Category { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("enclosure")]
        public XMLEnclosure Enclosure { get; set; }
    }

    public class XMLEnclosure
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }

}
