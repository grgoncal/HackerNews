using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HackerNews.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace HackerNews.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var buider = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                                  .AddEnvironmentVariables();

            Configuration = buider.Build();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDependencies(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HackerNewsApi");
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStatusCodePages();
        }
    }
}
