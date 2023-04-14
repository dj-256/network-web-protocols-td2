using MathNet.Numerics.Statistics;

namespace ContentLengthStats;

public class ContentLengthStatsResponse
{
    public ContentLengthStatsResponse(Dictionary<string, long> contentLengths)
    {
        Websites = contentLengths.Keys.ToList();
        ContentLengths = contentLengths;
        TotalContentLength = contentLengths.Values.Sum();
        var contentValuesDouble = contentLengths.Values.ToList().ConvertAll(e => (double)e);
        AverageContentLength = contentValuesDouble.Mean();
        MedianContentLength = contentValuesDouble.Median();
    }

    public List<string> Websites { get; set; }
    public Dictionary<string, long> ContentLengths { get; set; }
    public long TotalContentLength { get; set; }
    public double AverageContentLength { get; set; }
    public double MedianContentLength { get; set; }
}