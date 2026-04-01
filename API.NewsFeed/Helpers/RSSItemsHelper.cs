using API.NewsFeed.Models;
namespace API.NewsFeed.Helpers
{
    public static class RSSItemsHelper
    {
        public static List<Item> OrderItemsByPubDateAndDescription(IEnumerable<Item> items)
        {
            return items?.OrderByDescending(o => o.PubDate)
                .ThenByDescending(t => !string.IsNullOrEmpty(t.Description))
                .ToList() ?? new();
        }
    }
}
