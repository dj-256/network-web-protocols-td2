namespace HeaderStats.Applications.ServerStats;

public static class ServerHeaderManager
{
    public static async Task<ServerHeaderStatsResponse> GetServerHeadersForUrls(IEnumerable<string> urls,
        HttpClient sharedClient)
    {
        var websiteInfos = await GetInfoListForUrls(urls, sharedClient);
        var map = ComputeStatsFromInfoList(websiteInfos);
        return new ServerHeaderStatsResponse(urls, map);
    }

    private static Dictionary<string, int> ComputeStatsFromInfoList(IEnumerable<WebsiteInfo> websiteInfoList)
    {
        var repartition = new Dictionary<string, int> { { "Unknown", 0 } };
        foreach (var key in websiteInfoList.Select(websiteInfo => websiteInfo.SiteHeaders.Server.ToString()))
        {
            if (string.IsNullOrEmpty(key.Trim()))
            {
                repartition["Unknown"] += 1;
                continue;
            }

            if (repartition.ContainsKey(key))
            {
                repartition[key] += 1;
            }
            else
            {
                repartition.Add(key, 1);
            }
        }

        return repartition;
    }

    private static async Task<List<WebsiteInfo>> GetInfoListForUrls(IEnumerable<string> urls, HttpClient sharedClient)
    {
        var websiteInfos = new List<WebsiteInfo>();
        await Parallel.ForEachAsync(urls, async (website, token) =>
        {
            try
            {
                using var response = await sharedClient.GetAsync(website, token);
                Console.WriteLine($"{website}: {response.StatusCode} - {response.Headers.Server}");
                websiteInfos.Add(new WebsiteInfo(website, response.Headers));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{website}: {ex.Message}");
            }
        });
        return websiteInfos;
    }
}