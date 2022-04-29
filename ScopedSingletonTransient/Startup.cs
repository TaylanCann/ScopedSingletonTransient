using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopedSingletonTransient
{
    public class Startup
    {

        public class SingletonService
        {
            public int Counter;
        }

        public class ScopedService
        {
            public int Counter;
        }

        public class TransientService
        {
            public int Counter;
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<SingletonService>();
            services.AddScoped<ScopedService>();
            services.AddTransient<TransientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((ctx, next) => {
                // Get all the services and increase their counters...
                var singleton = ctx.RequestServices.GetRequiredService<SingletonService>();
                var scoped = ctx.RequestServices.GetRequiredService<ScopedService>();
                var transient = ctx.RequestServices.GetRequiredService<TransientService>();

                singleton.Counter++;
                scoped.Counter++;
                transient.Counter++;

                return next();
            });

            app.Run(async ctx =>
            {
                // ...then do it again...
                var singleton = ctx.RequestServices.GetRequiredService<SingletonService>();
                var scoped = ctx.RequestServices.GetRequiredService<ScopedService>();
                var transient = ctx.RequestServices.GetRequiredService<TransientService>();

                singleton.Counter++;
                scoped.Counter++;
                transient.Counter++;

                // ...and display the counter values.
                await ctx.Response.WriteAsync($"Singleton: {singleton.Counter}\n");
                await ctx.Response.WriteAsync($"Scoped: {scoped.Counter}\n");
                await ctx.Response.WriteAsync($"Transient: {transient.Counter}\n");
            });
        }x

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
