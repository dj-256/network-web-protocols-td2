namespace AgeStats;

public class AgeStatsManager
{
    public async Task<Dictionary<string, DateTimeOffset?>> GetAgeStatsForUrls(string[] urls, HttpClient sharedClient)
    {
        var websiteInfos = new Dictionary<string, DateTimeOffset?>();
        await Parallel.ForEachAsync(urls, async (website, token) =>
        {
            try
            {
                using var response = await sharedClient.GetAsync(website, token);
                Console.WriteLine($"{website}: {response.StatusCode} - {response.Content.Headers.LastModified}");
                var lastModifiedHeader = response.Content.Headers.LastModified;
                websiteInfos.Add(website, lastModifiedHeader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{website}: {ex.Message}");
            }
        });
        return websiteInfos;
    }
}