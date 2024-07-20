namespace API.NewsFeed.Helpers
{
    using API.NewsFeed.Models;
    using System.Xml;
    using System.Xml.Serialization;

    public static class XMLParser
    {
        public static XMLRssFeed ParseXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLRssFeed));
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                return (XMLRssFeed)serializer.Deserialize(reader);
            }
        }
    }
}
