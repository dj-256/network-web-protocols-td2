using ServerHeaderStats;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.MapPost("/server", async (HttpContext context) =>
{
    var urls = await context.Request.ReadFromJsonAsync<string[]>() ?? Array.Empty<string>();
    var headers = await ServerHeaderManager.GetServerHeadersForUrls(urls, new HttpClient());
    return Results.Ok(headers);
});

app.Run();