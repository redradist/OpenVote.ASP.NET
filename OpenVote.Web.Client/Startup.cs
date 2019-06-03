using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OpenVote.Web.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
                .SetMinimumLevel(LogLevel.Trace)
            );
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
