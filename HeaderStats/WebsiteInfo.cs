namespace HeaderStats;

public class WebsiteInfo
{
    public WebsiteInfo(string url, string serverHeader, string lastModifiedHeader)
    {
        Url = url;
        ServerHeader = serverHeader;
        LastModifiedHeader = lastModifiedHeader;
    }

    public string Url { get; set; }
    public string ServerHeader { get; set; }
    public string LastModifiedHeader { get; set; }
}