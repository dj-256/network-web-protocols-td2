using HeaderStats;

HttpClient sharedClient = new();

var websites = new[]
{
    "https://www.amazon.com/",
    "https://www.google.com/",
    "https://www.youtube.com/",
    "https://www.facebook.com/",
    "https://www.wikipedia.org/",
    "https://www.reddit.com/",
    "https://www.twitter.com/",
    "https://www.instagram.com/",
    "https://www.linkedin.com/",
    "https://www.netflix.com/",
    "https://www.apple.com/",
    "https://www.yahoo.com/",
    "https://www.bing.com/",
    "https://www.pinterest.com/",
    "https://www.ebay.com/",
    "https://www.microsoft.com/",
    "https://www.cnn.com/",
    "https://www.imdb.com/",
    "https://www.nytimes.com/",
    "https://www.github.com/",
    "https://www.espn.com/",
    "https://www.paypal.com/",
    "https://www.tumblr.com/",
    "https://www.quora.com/",
    "https://www.huffpost.com/",
    "https://www.nationalgeographic.com/",
    "https://www.etsy.com/",
    "https://www.weather.com/",
    "https://www.foxnews.com/",
    "https://www.nba.com/",
    "https://www.cnbc.com/",
    "https://www.bbc.com/",
    "https://www.webmd.com/",
    "https://www.bloomberg.com/",
    "https://www.medicalnewstoday.com/",
    "https://www.wsj.com/",
    "https://www.techcrunch.com/",
    "https://www.wired.com/",
    "https://www.merriam-webster.com/",
    "https://www.booking.com/",
    "https://www.airbnb.com/",
    "https://www.netflix.com/",
    "https://www.hulu.com/",
    "https://www.spotify.com/",
    "https://www.soundcloud.com/",
    "https://www.twitch.tv/",
    "https://www.vimeo.com/",
    "https://www.dribbble.com/",
    "https://www.behance.net/",
    "https://www.medium.com/",
    "https://www.stackoverflow.com/",
    "https://www.github.io/",
    "https://www.codepen.io/",
    "https://www.lemonde.fr/",
    "https://www.liberation.fr/",
    "https://www.lefigaro.fr/",
    "https://www.lequipe.fr/",
    "https://www.francetvinfo.fr/",
    "https://www.cdiscount.com/",
    "https://www.fnac.com/",
    "https://www.darty.com/",
    "https://www.laredoute.fr/",
    "https://www.vinted.fr/",
    "https://www.alloresto.fr/",
    "https://www.ouibus.com/",
    "https://www.sncf.com/",
    "https://www.ledauphine.com/",
    "https://www.laposte.fr/",
    "https://www.lcl.fr/",
    "https://www.societegenerale.fr/",
    "https://www.credit-agricole.fr/",
    "https://www.banquepopulaire.fr/",
    "https://www.caisse-epargne.fr/",
    "https://www.orange.fr/",
    "https://www.sfr.fr/",
    "https://www.bouyguestelecom.fr/",
    "https://www.larousse.fr/",
    "https://www.franceinter.fr/",
    "https://www.eurosport.fr/",
    "https://www.journaldunet.com/",
    "https://www.doctolib.fr/",
    "https://www.pole-emploi.fr/",
    "https://www.gouv.fr/"
};


var pages = new[]
{
    "https://developer.android.com/docs/quality-guidelines/build-for-billions/ui",
    "https://developer.android.com/docs/quality-guidelines/car-app-quality",
    "https://developer.android.com/docs/quality-guidelines/build-for-billions",
    "https://developer.android.com/docs/quality-guidelines/large-screen-app-quality",
    "https://developer.android.com/docs",
    "https://developer.android.com/docs/quality-guidelines/wear-app-quality",
    "https://developer.android.com/docs/quality-guidelines/build-for-billions/device-capacity",
    "https://developer.android.com/docs/quality-guidelines/core-app-quality",
    "https://developer.android.com/docs/quality-guidelines/build-for-billions/data-cost",
    "https://developer.android.com/docs/quality-guidelines/tv-app-quality",
    "https://developer.android.com/docs/quality-guidelines/build-for-billions/battery-consumption",
    "https://developer.android.com/docs/quality-guidelines/build-for-billions/connectivity",
    "https://developer.android.com/docs/quality-guidelines/2021/02"
};

var serverStats = new ServerStats();
var headers = await serverStats.FetchInfo(websites, sharedClient);

var serverHeaderStats = serverStats.ComputeServerHeaderRepartition(headers);
serverStats.PrintRepartition(serverHeaderStats);

var ageInfoList = await serverStats.FetchInfo(pages, sharedClient);
var modifiedStats = serverStats.ComputeLastModifiedHeaderMap(ageInfoList);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.MapGet("/api/stats/server", () => serverHeaderStats);
app.MapGet("/api/stats/modified", () => modifiedStats);


app.Run();