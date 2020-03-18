using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlattSampleApp.ApiServices.StarWars;
using PlattSampleApp.Adapters;
using PlattSampleApp.Adapters.StarWars;

namespace PlattSampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("swApiClient", client =>
            {
                client.BaseAddress = new Uri("https://swapi.co");
            });

            services.AddTransient<ISwPlanetApiService, SwPlanetApiService>();
            services.AddTransient<ISwVehicleApiService, SwVehicleApiService>();
            services.AddTransient<ISwPersonApiService, SwPersonApiService>();
            services.AddTransient<ISwStarshipService, SwStarshipService>();
            services.AddTransient<IStarWarsAdapter, StarWarsAdapter>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
