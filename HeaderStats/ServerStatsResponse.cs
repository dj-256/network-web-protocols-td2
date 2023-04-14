namespace HeaderStats;

public class ServerStatsResponse
{
    public ServerStatsResponse(List<string> websites, Dictionary<string, int> serverStats)
    {
        Websites = websites;
        ServerStats = serverStats;
    }

    List<string> Websites { get; set; }
    Dictionary<string, int> ServerStats { get; set; }
}