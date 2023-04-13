using System.Net.Http.Headers;

namespace HeaderStats;

public class ServerStats
{
    private readonly List<HttpResponseHeaders> _headers;
    public readonly Dictionary<string, double> Repartition = new();

    public ServerStats(List<HttpResponseHeaders> headers)
    {
        _headers = headers;
        Repartition.Add("Unknown", 0);
    }

    public void ComputeRepartition()
    {
        foreach (var key in _headers.Select(header => header.Server.ToString()))
        {
            if (String.IsNullOrEmpty(key.Trim()))
            {
                Repartition["Unknown"] += 1;
                continue;
            }

            if (Repartition.ContainsKey(key))
            {
                Repartition[key] += 1;
            }
            else
            {
                Repartition.Add(key, 1);
            }
        }
    }

    public void PrintRepartition()
    {
        foreach (var server in Repartition)
        {
            Console.WriteLine($"{server.Key} : {server.Value}");
        }
    }
}