using API.NewsFeed.Models;

namespace API.NewsFeed.Helpers
{
    public static class JsonToFileHelper
    {
        public static List<Item>? ReadJsonFromFile(string feedName, string folderName = "CachedJsonData")
        {
            var filePath = $"{folderName}/{feedName}.json";
            if (GetFileAgeInHours(filePath) >= 6)
                File.Delete(filePath);

            if (!File.Exists(filePath))
                return null;

            string jsonContent = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<List<Item>>(jsonContent) ?? null;
        }

        public static void WriteJsonToFile(string feedName, List<Item> items, string folderName = "CachedJsonData")
        {
            var filePath = $"{folderName}/{feedName}.json";
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(items);
            File.WriteAllText(filePath, jsonContent);
        }

        private static double GetFileAgeInHours(string filePath)
        {
            if (!File.Exists(filePath))
                return 0;

            DateTime creationTime = File.GetCreationTime(filePath);
            TimeSpan age = DateTime.Now - creationTime;
            return age.TotalHours;
        }
    }
}
