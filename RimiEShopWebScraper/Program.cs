using RimiEShopWebScraper;
using RimiEShopWebScraper.Data;
using RimiEShopWebScraper.Jobs;
using RimiEShopWebScraper.Services;
using RimiEShopWebScraper.Services.Interfaces;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ScrapingJob>()
            .AddSingleton<IScrapingService, ScrapingService>();
        //.AddDbContext<AppContext>();
        services.AddDbContext<AppDbContext>();
        //services.AddSingleton<ScrapingService>();
    })
    .Build();

await host.RunAsync();
