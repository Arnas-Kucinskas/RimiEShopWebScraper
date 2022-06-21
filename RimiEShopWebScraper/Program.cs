using RimiEShopWebScraper;
using RimiEShopWebScraper.Data;
using RimiEShopWebScraper.Jobs;
using RimiEShopWebScraper.Repository.Interfaces;
using RimiEShopWebScraper.Services;
using RimiEShopWebScraper.Services.Interfaces;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddDbContext<AppDbContext>();
        services.AddDbContextFactory<AppDbContext>();
        services.AddHostedService<ScrapingJob>()
            
            .AddSingleton<IScrapingService, ScrapingService>()
            .AddSingleton<IProductsRepository, ProductsRepository>()
            ;
        //.AddDbContext<AppContext>();
        
        //services.AddSingleton<ScrapingService>();
    })
    .Build();

await host.RunAsync();
