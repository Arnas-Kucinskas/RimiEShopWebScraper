using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper
{
    internal class WebPageReader
    {
        public HtmlDocument Doc { get; set; }

        private string _categoryUrl = string.Empty;
        private string _baseUrl = string.Empty;
        public WebPageReader(string baseCategoryUrl, string baseUrl)
        {
            _categoryUrl = baseCategoryUrl;
            _baseUrl = baseUrl;
            
        }

        public bool SeekPage()
        {
            if (Doc == null)
            {
                LoadInitialPage();
                return true;
            }

            var nextPageNode = Doc.DocumentNode.SelectNodes("//li[@class='pagination__item -chevron']")
                .Last()
                .SelectSingleNode(".//a");

            if (nextPageNode == null)
            {
                return false;
            }
            var web = new HtmlWeb();
            Doc = web.Load(_baseUrl + nextPageNode.Attributes["href"].Value);

            return true;

        }

        public void LoadInitialPage()
        {
            var web = new HtmlWeb();
            Doc = web.Load(_categoryUrl);
        }

    }
}
