using AgeStats;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var ageStatsManager = new AgeStatsManager();

app.MapGet("/", () => "Hello World!");

app.MapPost("/age", async (HttpContext context) =>
{
    var urls = await context.Request.ReadFromJsonAsync<string[]>() ?? Array.Empty<string>();
    var headers = await ageStatsManager.GetAgeStatsForUrls(urls, new HttpClient());
    return Results.Ok(headers);
});

app.Run();