using System.Reflection;
using Microsoft.AspNetCore.StaticFiles;

namespace StreamSplitter.WebUI;

public delegate Stream? EmbeddedResourceProvider(string path);

public interface IEmbeddedResourceRouteBuilder
{
    public EmbeddedResourceProvider Build();
    public IEmbeddedResourceRouteBuilder Map(string pathPrefix, string resourcePrefix, Assembly resourceAssembly);
}

public class EmbeddedResourceMiddleware
{
    private RequestDelegate Next { get; }
    private EmbeddedResourceProvider ResourceProvider { get; }

    public EmbeddedResourceMiddleware(RequestDelegate next, EmbeddedResourceProvider resourceProvider)
    {
        Next = next;
        ResourceProvider = resourceProvider;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        string path = httpContext.Request.Path;

        await using Stream? resourceStream = ResourceProvider(path);

        if (resourceStream == null)
        {
            await Next(httpContext);
            return;
        }

        FileExtensionContentTypeProvider provider = new();

        if (provider.TryGetContentType(path, out string? contentType))
            httpContext.Response.ContentType = contentType;

        await resourceStream.CopyToAsync(httpContext.Response.Body);
    }
}

public static class EmbeddedResourceMiddlewareExtensions
{
    private class EmbeddedResourceRouteBuilder : IEmbeddedResourceRouteBuilder
    {
        private class Route
        {
            public Route(string pathPrefix, string resourcePrefix, Assembly resourceAssembly)
            {
                PathPrefix = pathPrefix;
                ResourcePrefix = resourcePrefix;
                ResourceAssembly = resourceAssembly;
            }

            public string PathPrefix { get; }
            public string ResourcePrefix { get; }
            public Assembly ResourceAssembly { get; }
        }

        private List<Route> Routes { get; } = new();

        public EmbeddedResourceProvider Build()
        {
            return path =>
            {
                foreach (Route route in Routes)
                {
                    if (!path.StartsWith(route.PathPrefix))
                        continue;

                    string resourcePath = path[route.PathPrefix.Length..].Replace('/', '.');
                    string resourceName = $"{route.ResourcePrefix}{resourcePath}";
                    Stream? resourceStream = route.ResourceAssembly.GetManifestResourceStream(resourceName);

                    if (resourceStream != null)
                        return resourceStream;
                }

                return null;
            };
        }

        public IEmbeddedResourceRouteBuilder Map(string pathPrefix, string resourcePrefix, Assembly resourceAssembly)
        {
            Routes.Add(new Route(pathPrefix, resourcePrefix, resourceAssembly));
            return this;
        }
    }

    public static IApplicationBuilder UseEmbeddedResources(this IApplicationBuilder app, Action<IEmbeddedResourceRouteBuilder> configure)
    {
        EmbeddedResourceRouteBuilder routeBuilder = new();
        configure(routeBuilder);

        EmbeddedResourceProvider resourceProvider = routeBuilder.Build();
        return app.UseMiddleware<EmbeddedResourceMiddleware>(resourceProvider);
    }

    public static IEmbeddedResourceRouteBuilder MapWebRoot(this IEmbeddedResourceRouteBuilder builder, string pathPrefix) =>
        builder.Map(pathPrefix, $"{nameof(StreamSplitter)}.{nameof(WebUI)}.wwwroot", Assembly.GetExecutingAssembly());
}