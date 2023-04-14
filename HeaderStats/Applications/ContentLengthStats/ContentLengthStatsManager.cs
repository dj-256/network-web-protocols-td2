namespace HeaderStats.Applications.ContentLengthStats;

public class ContentLengthStatsManager
{
    public static async Task<ContentLengthStatsResponse> GetContentLengthStatsForUrls(IEnumerable<string> urls,
        HttpClient sharedClient)
    {
        var contentLengths = await FetchWebsiteInfoFromUrls(urls, sharedClient);
        return new ContentLengthStatsResponse(contentLengths);
    }

    private static async Task<Dictionary<string, long>> FetchWebsiteInfoFromUrls(IEnumerable<string> urls,
        HttpClient sharedClient)
    {
        var contentLengths = new Dictionary<string, long>();
        await Parallel.ForEachAsync(urls, async (website, token) =>
        {
            try
            {
                using var response = await sharedClient.GetAsync(website, token);
                var contentLength = response.Content.Headers.ContentLength ?? 0;
                Console.WriteLine($"{website}: {response.StatusCode} - {contentLength}");
                contentLengths.Add(website, contentLength);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{website}: {ex.Message}");
            }
        });
        return contentLengths;
    }
}