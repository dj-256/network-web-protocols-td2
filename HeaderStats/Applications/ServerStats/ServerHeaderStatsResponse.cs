namespace HeaderStats.Applications.ServerStats;

public class ServerHeaderStatsResponse
{
    public ServerHeaderStatsResponse(IEnumerable<string> websites, Dictionary<string, int> serverStats)
    {
        Websites = websites;
        ServerStats = serverStats;
    }

    public IEnumerable<string> Websites { get; set; }
    public Dictionary<string, int> ServerStats { get; set; }
}