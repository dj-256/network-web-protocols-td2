using System.Net.Http.Headers;

namespace HeaderStats;

public class WebsiteInfo
{
    public WebsiteInfo(string url, HttpResponseHeaders siteHeaders)
    {
        Url = url;
        SiteHeaders = siteHeaders;
    }

    public string Url { get; set; }
    public HttpResponseHeaders SiteHeaders { get; set; }
}