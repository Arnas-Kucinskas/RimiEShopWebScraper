using HtmlAgilityPack;
using RimiEShopWebScraper.Models;
using RimiEShopWebScraper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Jobs
{
    internal class ScrapingJob : BackgroundService
    {
        private readonly IScrapingService _scrapingService;

        public ScrapingJob(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            while (!stoppingToken.IsCancellationRequested)
            {
                await _scrapingService.ScrapeEverything();
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Console.WriteLine("a");
                await Task.Delay(1000, stoppingToken);

                

            }

            
        }

    }
}
