using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NCore.Base.WebAuth.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var authService = new AuthBuilder().Build(services);
            services.AddSingleton(authService);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerfactory)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}