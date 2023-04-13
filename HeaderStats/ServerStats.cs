namespace HeaderStats;

public class ServerStats
{
    public Dictionary<string, int> ComputeServerHeaderRepartition(List<WebsiteInfo> websiteInfoList)
    {
        var repartition = new Dictionary<string, int>();
        foreach (var websiteInfo in websiteInfoList)
        {
            var key = websiteInfo.ServerHeader;
            if (String.IsNullOrEmpty(key.Trim()))
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

    public Dictionary<string, string> ComputeLastModifiedHeaderMap(List<WebsiteInfo> websiteInfoList)
    {
        var lastModifiedHeaderMap = new Dictionary<string, string>();
        foreach (var websiteInfo in websiteInfoList)
        {
            var key = websiteInfo.Url;
            var value = websiteInfo.LastModifiedHeader;
            lastModifiedHeaderMap[key] = value;
        }

        return lastModifiedHeaderMap;
    }

    public void PrintRepartition(Dictionary<string, int> repartition)
    {
        foreach (var server in repartition)
        {
            Console.WriteLine($"{server.Key} : {server.Value}");
        }
    }

    public async Task<List<WebsiteInfo>> FetchInfo(string[] urls, HttpClient sharedClient)
    {
        var websiteInfos = new List<WebsiteInfo>();
        await Parallel.ForEachAsync(urls, async (website, token) =>
        {
            try
            {
                using var response = await sharedClient.GetAsync(website, token);
                Console.WriteLine($"{website}: {response.StatusCode} - {response.Headers.Server}");
                var serverHeader = response.Headers.Server.ToString();
                if (String.IsNullOrEmpty(serverHeader.Trim()))
                    serverHeader = "N/A";
                var lastModifiedHeader = response.Content.Headers.LastModified?.ToString("o") ?? "N/A";
                websiteInfos.Add(new WebsiteInfo(website, serverHeader, lastModifiedHeader));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{website}: {ex.Message}");
            }
        });
        return websiteInfos;
    }
}