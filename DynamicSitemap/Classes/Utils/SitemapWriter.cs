using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DynamicSitemap.Classes.Utils
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Sitemap
    {
        private List<SitemapLocation> map;

        public Sitemap()
        {
            map = new List<SitemapLocation>();
        }

        [XmlElement("url")]
        public List<SitemapLocation> Locations
        {
            get
            {
                return map;
            }
            set
            {
                if (value == null)
                    return;
                map = value;
            }
        }

        public int Add(SitemapLocation item)
        {
            map.Add(item);
            return map.Count;
        }

        public void AddRange(IEnumerable<SitemapLocation> locs)
        {
            map.AddRange(locs);
        }

        public string WriteSitemapToString()
        {
            using (StringWriter sw = new Utf8StringWriter())
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("image", "http://www.google.com/schemas/sitemap-image/1.1");

                XmlSerializer xs = new XmlSerializer(typeof(Sitemap));
                xs.Serialize(sw, this, ns);
                return sw.GetStringBuilder().ToString();
            }
        }

        public void WriteSitemapToFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("image", "http://www.google.com/schemas/sitemap-image/1.1");

                XmlSerializer xs = new XmlSerializer(typeof(Sitemap));
                xs.Serialize(fs, this, ns);
            }
        }

    //    Override UTF-16 as google wont work without UTF-8
        public class Utf8StringWriter : StringWriter
            {
                public override Encoding Encoding
                {
                    get { return new UTF8Encoding(false); }
                }
            }
        }
}
