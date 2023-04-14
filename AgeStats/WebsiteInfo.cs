using System.Net.Http.Headers;

namespace AgeStats;

public class WebsiteInfo
{
    public WebsiteInfo(string url, HttpHeaders siteHeaders)
    {
        Url = url;
        SiteHeaders = siteHeaders;
    }

    public string Url { get; set; }
    public HttpHeaders SiteHeaders { get; set; }
}