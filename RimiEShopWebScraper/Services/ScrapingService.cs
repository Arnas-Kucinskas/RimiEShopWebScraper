using HtmlAgilityPack;
using RimiEShopWebScraper.Models;
using RimiEShopWebScraper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Services
{
    internal class ScrapingService : IScrapingService
    {
        private string _baseUrl = "https://www.rimi.lt";
        private List<string> _baseCategories = new List<string>()
            {
                "https://www.rimi.lt/e-parduotuve/lt/produktai/proginiai-gaminiai-uzsakomieji-/c/SH-34",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/vaisiai-darzoves-ir-geles/c/SH-15",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/-vikis-prekiu-krautuvele/c/SH-18",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/veganams-ir-vegetarams/c/SH-77",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/pieno-produktai-kiausiniai-ir-suris/c/SH-11",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/duonos-gaminiai-ir-konditerija/c/SH-3",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/mesa-zuvys-ir-kulinarija/c/SH-9",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/bakaleja/c/SH-2",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/vaiku-ir-kudikiu-prekes/c/SH-7",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/gerimai/c/SH-4",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/alkoholiniai-gerimai/c/SH-1",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/kosmetika-ir-higiena/c/SH-6",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/buitines-chemijos-ir-valymo-priemones/c/SH-16",
                "https://www.rimi.lt/e-parduotuve/lt/produktai/namu-ukio-gyvunu-ir-laisvalaikio-prekes/c/SH-10"
            };
        public int FailedCount { get; set; }

        public ScrapingService()
        {

        }

        public async Task ScrapeEverything()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://www.rimi.lt/e-parduotuve/lt/produktai/vaisiai-darzoves-ir-geles/c/SH-15");

            //var linkNodes = doc.DocumentNode.SelectNodes("//li[@class='product-grid__item']/div/div[@class='card__details']");


            var products = new Dictionary<long, Product>();



            foreach (var baseCategory in _baseCategories)
            {
                ScrapeWholeBaseCategory(baseCategory, products);
            }
            
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Value.Id} Name: {product.Value.Name} Price: {product.Value.PriceMinorCurrency} SalePrice: {product.Value.PriceOnSaleMinorcurrency}");
            }

            var a = "";
        }

        private void ScrapeWholeBaseCategory(string url, Dictionary<long, Product> products)
        {
            //var web = new HtmlWeb();
            //var doc = web.Load(baseCategory.url);
            var pageReader = new WebPageReader(url, _baseUrl);

            while (pageReader.SeekPage())
            {
                var linkNodes = pageReader.Doc.DocumentNode.SelectNodes("//li[@class='product-grid__item']/div[contains(@class, 'card')]");
                foreach (var node in linkNodes)
                {
                    var product = new Product();
                    long.TryParse(node.Attributes["data-product-code"].Value, out var productId);


                    product.Id = productId;
                    if (product.Id == 213328)
                    {
                        var a = "";
                    }
                    product.ImageUrl = _baseUrl + node.SelectSingleNode(".//a[contains(@class, 'card__url')]").Attributes["href"].Value;
                    product.Name = node.SelectSingleNode(".//div[@class='card__details']/p[@class='card__name']").InnerText;
                    product.ScrapeDate = DateOnly.FromDateTime(DateTime.Now);
                    try
                    {
                        //product.Category = baseCategory.productCategory;


                        if (!IsProductAvailable(node, product))
                        {
                            products[product.Id] = product;
                            continue;
                        }
                        

                        SetSaleType(node, product);

                        switch (product.SaleType)
                        {
                            case SaleType.NoSale:
                                ParseNoSalePrice(node, product);
                                break;
                            case SaleType.RegularSale:
                                ParseRegularSalePrice(node, product);
                                break;
                            case SaleType.TwoForOne:
                                break;
                            default:
                                break;
                        }

                        //var xPAthNavigator = node.
                        //var yhg = xPAthNavigator.Select("//p[@class='card__name']");
                        products[product.Id] = product;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(FailedCount++);
                        //throw;
                    }

                }
            }
            
        }

        private bool IsProductAvailable(HtmlNode node, Product product)
        {
            //a[contains(@class, 'card__price-wrapper')]
            var productUnavailalbe = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[contains(@class, 'card__price-wrapper')]/p[@class='card__price-per']")?.InnerText;
            if (productUnavailalbe != null)
            {
                product.IsProductAvailalbe = false;
                Console.WriteLine($"ProductName: {product.Name} . Error: {productUnavailalbe}" );
                return false;
            }
            return true;
        }

        private static void ParseRegularSalePrice(HtmlNode node, Product product)
        {
            var saleMajorCurency = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[@class='card__price-wrapper -has-discount']/div[@class='price-tag card__price']/span").InnerText;
            var saleMinorCurency = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[@class='card__price-wrapper -has-discount']/div[@class='price-tag card__price']/div/sup").InnerText;
            var originalPrice = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[@class='card__price-wrapper -has-discount']/div/div[@class='old-price-tag card__old-price']/span").InnerText;
            //var originalMinorCurency = "";

            long.TryParse(saleMajorCurency + saleMinorCurency, out var salePrice);
            product.PriceOnSaleMinorcurrency = salePrice;
            product.PriceMinorCurrency = ParseOriginalPrice(originalPrice);
        }

        private static void ParseNoSalePrice(HtmlNode node, Product product)
        {
            var majorCurency = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[@class='card__price-wrapper']/div[@class='price-tag card__price']/span").InnerText;
            var minorCurency = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[@class='card__price-wrapper']/div[@class='price-tag card__price']/div/sup").InnerText;
            long.TryParse(majorCurency + minorCurency, out var basePrice);
            product.PriceMinorCurrency = basePrice;
        }

        private static void SetSaleType(HtmlNode node, Product product)
        {
   
            var twoForOneBadge = node.SelectSingleNode(".//div[@class='card__image-wrapper']/div/div[@class='price-badge']/span[@class='price-badge__text']/span")?.InnerText;
            if (twoForOneBadge != null)
            {
                product.SaleType = SaleType.TwoForOne;
                product.IsOnSale = true;
                Console.WriteLine(twoForOneBadge);
                return;
            }


            var primaryMajorCurency = node.SelectSingleNode(".//div[@class='card__details']/div[@class='card__details-inner']/div[@class='card__price-wrapper']/div[@class='price-tag card__price']/span");
            if (primaryMajorCurency == null)
            {
                product.SaleType = SaleType.RegularSale;
                product.IsOnSale = true;
                return;
            }



            product.SaleType = SaleType.NoSale;
        }

        private static long ParseOriginalPrice(string originalPrice)
        {
            var symbolsToRemove = new char[] { ',', '€' };
            var priceAsString =  new string(originalPrice.Except(symbolsToRemove).ToArray());
            long.TryParse(priceAsString, out var price);
            if (price == 0)
            {
                var a = "";
            }
            return price;
        }



    }
}
