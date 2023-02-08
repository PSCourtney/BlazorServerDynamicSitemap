using DynamicSitemap.Classes.Utils;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace DynamicSitemap.Helpers
{
    public class SitemapXml
    {
        public Classes.Utils.Sitemap? Sitemap { get; set; }

        private class WebPage
        {
            public string? Domain { get; set; }
            public string? Prefix { get; set; }
            public string? Path { get; set; }
        }

        public string Generate()
        {
            Sitemap = new Classes.Utils.Sitemap();
            //Dummy data, like from a database. Dynamic pages
            List<WebPage> classData = new List<WebPage>
            {
                new WebPage { Domain = "example1.com", Prefix = "https", Path = "/path1" },
                new WebPage { Domain = "example2.com", Prefix = "https", Path = "/path2" },
                new WebPage { Domain = "example3.com", Prefix = "https", Path = "/path3" }
            };

            foreach (var item in classData)
            {
                Sitemap.Add(new SitemapLocation
                {
                    ChangeFrequency = SitemapLocation.eChangeFrequency.yearly,
                    Url = $"{item.Prefix}://{item.Domain}{item.Path}"
                });
            }

            // Get all the components wich are not dynamic who have a base class of ComponentBase
            var components = Assembly.GetExecutingAssembly().ExportedTypes
                .Where(t => typeof(ComponentBase).IsAssignableFrom(t)).ToList();

            var routables = components.Where(c => c.GetCustomAttributes(inherit: true)
                .OfType<RouteAttribute>().Any());

            foreach (var routable in routables)
            {
                Sitemap.Add(new SitemapLocation
                {
                    ChangeFrequency = SitemapLocation.eChangeFrequency.yearly,
                    Url = $"https://example3.com/{routable}"
                });
            }

            return Sitemap.WriteSitemapToString();
        }
    }
}