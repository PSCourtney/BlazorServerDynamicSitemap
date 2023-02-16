# Dynamic Sitemap for Blazor Server
A dynamic sitemap xml generator for Blazor Server.  

![](https://i.imgur.com/yLXVXc0.jpeg)


## Usage
Make sitemap.xml a navigible endpoint in `Program.cs`
```cs
app.UseEndpoints(endpoints =>
{
    app.MapGet("/sitemap.xml", (SitemapXml sitemapXml) => sitemapXml.Generate());
});

```

Decide on how to build up your `Classes.Utils.Sitemap` class and call it in `sitemapXml.Generate()`.  Below a `List<WebPage>` is used to fill the sitemap with dummy endpoints. This could be done statically like this or via a database call.





```cs
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

```

It is also possible to call a list of the websites page or component endpoints.

```cs
 // Get all the components wich are not dynamic who have a base class of ComponentBase
 
var components = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => typeof(ComponentBase).IsAssignableFrom(t)).ToList();
var routables = components.Where(c => c.GetCustomAttributes(inherit: true).OfType<RouteAttribute>().Any());

foreach (var routable in routables)
{
	Sitemap.Add(new SitemapLocation
	{
		ChangeFrequency = SitemapLocation.eChangeFrequency.yearly,
		Url = $"https://example3.com/{routable}"
	});
}
```

