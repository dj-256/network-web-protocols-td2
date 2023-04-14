using ContentLengthStats;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/content-length", async (HttpContext context) =>
{
    var urls = await context.Request.ReadFromJsonAsync<string[]>() ?? Array.Empty<string>();
    var contentLengths = await ContentLengthStatsManager.GetContentLengthStatsForUrls(urls, new HttpClient());
    return Results.Ok(contentLengths);
});

app.Run();