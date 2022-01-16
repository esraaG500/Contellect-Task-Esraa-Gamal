using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contellect.ContactApp.DB.AppContext;
using Contellect.ContactApp.Core.IReposatory;
using Contellect.ContactApp.Reposatory;
using Contellect.ContactApp.Core.IServices;
using Contellect.ContactApp.Service;
using Contellect.ContactApp.Hubs;

namespace Contellect.ContactApp
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
            services.AddSignalR();

            //connection of DB
            services.AddDbContext<ContellectContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("ContactDB")));

            services.AddControllersWithViews();
            services.AddSession();
            //injectServices
            InjectServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=LogIn}/{action=logInPage}/{id?}");
                    //pattern: "{controller=Contact}/{action=Index}/{id?}");
            //pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapHub<DataHub>("/DataHub");
            });
        }

        void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IContactReposatory, ContactReposatory>();
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
