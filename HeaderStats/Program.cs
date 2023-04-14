using HeaderStats.Applications.AgeStats;
using HeaderStats.Applications.ContentLengthStats;
using HeaderStats.Applications.ServerStats;

HttpClient sharedClient = new();
sharedClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseCors();

app.MapGet("/", () =>
{
    var fileContent = File.ReadAllText("./index.html"); // read the contents of the HTML file
    return Results.Text(fileContent, "text/html"); // return the HTML file with a MIME type of text/html
});

app.MapGet("/age", () =>
{
    var fileContent = File.ReadAllText("./age.html"); // read the contents of the HTML file
    return Results.Text(fileContent, "text/html"); // return the HTML file with a MIME type of text/html
});

app.MapPost("/api/stats/content-length", async (HttpContext context) =>
{
    var urls = await context.Request.ReadFromJsonAsync<string[]>() ?? Array.Empty<string>();
    var stats = await ContentLengthStatsManager.GetContentLengthStatsForUrls(urls, sharedClient);
    return stats;
});

app.MapPost("/api/stats/age", async (HttpContext context) =>
{
    var urls = await context.Request.ReadFromJsonAsync<string[]>() ?? Array.Empty<string>();
    var stats = await AgeStatsManager.GetAgeStatsForUrls(urls, sharedClient);
    return stats;
});

app.MapPost("/api/stats/server", async (HttpContext context) =>
{
    var urls = await context.Request.ReadFromJsonAsync<string[]>() ?? Array.Empty<string>();
    var stats = await ServerHeaderManager.GetServerHeadersForUrls(urls, sharedClient);
    return stats;
});


app.Run();