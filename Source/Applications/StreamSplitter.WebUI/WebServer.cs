using System.Security.Cryptography.X509Certificates;
using Gemstone.Configuration;
using Microsoft.Extensions.FileProviders;

namespace StreamSplitter.WebUI;

public class WebServerConfiguration
{
    public WebHosting Hosting { get; set; } = default!;
    
    //private class DefaultAuthenticator : IAuthenticator
    //{
    //    public Task<long?> AuthenticateAsync(string username, string password) => throw new NotImplementedException();
    //    public Task<UserData> GetUserDataAsync(long userID) => throw new NotImplementedException();
    //}

    public Func<X509Certificate2?> CertificateSelector { get; set; } = () => null;
    
    //public IAuthenticator Authenticator { get; set; } = new DefaultAuthenticator();
}

public class WebServer(WebServerConfiguration configuration)
{
    private Func<Task>? m_stopFunc;

    private WebServerConfiguration Configuration { get; } = configuration;

    public async Task StartAsync()
    {
        using CancellationTokenSource tokenSource = new();
        TaskCompletionSource<bool> completionSource = new();

        Task stopFunc()
        {
            try
            {
                // ReSharper disable once AccessToDisposedClosure
                tokenSource.Cancel();
            }
            catch (ObjectDisposedException)
            {
                // Ignore object disposed
            }

            return completionSource.Task;
        }

        Func<Task>? memberFunc = Interlocked.CompareExchange(ref m_stopFunc, stopFunc, null);

        if (memberFunc != null)
            return;

        IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

        string[] urls = Configuration.Hosting.HostURLs.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string webRoot = Configuration.Hosting.WebRoot;
        hostBuilder.ConfigureWebHostDefaults(webBuilder => ConfigureWebHost(webBuilder, urls, webRoot));

        IHost host = hostBuilder.Build();
        await host.RunAsync(tokenSource.Token);
        completionSource.SetResult(true);
    }

    public async Task StopAsync()
    {
        Func<Task>? stopFunc = Interlocked.Exchange(ref m_stopFunc, null);
        Task stopTask = stopFunc?.Invoke() ?? Task.CompletedTask;
        await stopTask;
    }

    private void ConfigureWebHost(IWebHostBuilder webBuilder, string[] urls, string webRoot)
    {
        webBuilder.ConfigureKestrel(
            options => options.ConfigureHttpsDefaults(
                op => op.ServerCertificate = Configuration.CertificateSelector()));

        webBuilder.UseUrls(urls);
        webBuilder.UseWebRoot(webRoot);
        webBuilder.ConfigureServices((_, serviceCollection) => ConfigureServices(serviceCollection));
        webBuilder.Configure((context, app) => Configure(app, context.HostingEnvironment));
    }

    private void ConfigureServices(IServiceCollection services)
    {
        //HttpAuthenticator httpAuthenticator = new HttpAuthenticator(Configuration.Authenticator);

        //services.AddSingleton(typeof(HttpAuthenticator), httpAuthenticator);
        //services.AddSingleton(typeof(IAuthenticator), Configuration.Authenticator);

        // TODO: Add login page and authentication services
        //services
        //    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie(options =>
        //    {
        //        options.LoginPath = "/Login";
        //        options.AccessDeniedPath = "/Forbidden";
        //    });

        const string Administrators = nameof(Administrators);
        const string Editors = nameof(Editors);
        const string Viewers = nameof(Viewers);

        //services.AddAuthorization(options =>
        //{
        //    string administratorRole = Role.Administrator.ToString();
        //    string editorRole = Role.Editor.ToString();
        //    string viewerRole = Role.Viewer.ToString();
        //    options.AddPolicy(Administrators, policy => policy.RequireRole(administratorRole, editorRole, viewerRole));
        //    options.AddPolicy(Editors, policy => policy.RequireRole(editorRole, viewerRole));
        //    options.AddPolicy(Viewers, policy => policy.RequireRole(viewerRole));
        //    options.FallbackPolicy = options.GetPolicy(Administrators);
        //});

        services.AddRazorPages(options =>
        {
            //options.Conventions.AuthorizePage("/Index");
            options.Conventions.AllowAnonymousToPage("/Index");

            //options.Conventions.AuthorizePage("/Devices", Viewers);
            //options.Conventions.AuthorizePage("/Device", Editors);
            //options.Conventions.AuthorizePage("/Users", Administrators);
            //options.Conventions.AuthorizePage("/Groups", Administrators);

            //options.Conventions.AllowAnonymousToPage("/Forbidden");
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseAuthentication();

        if (!TryUseStaticFiles(app, env))
        {
            string webRoot = Configuration.Hosting.WebRoot;
            app.UseEmbeddedResources(routes => routes.MapWebRoot(webRoot));
        }

        //app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapRazorPages());
    }

    private static bool TryUseStaticFiles(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
            return false;

        StaticFileOptions options = new();
        string wwwroot = Settings.Default.WebHosting.WebRoot;
        options.FileProvider = new PhysicalFileProvider(wwwroot);
        app.UseStaticFiles(options);
        
        return true;
    }

}
