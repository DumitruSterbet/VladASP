using Microsoft.AspNetCore.Authentication.Cookies;
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
using VladASP.Models;

namespace VladASP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<FlyContext>(options =>
                options.UseSqlServer(connection));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options => //CookieAuthenticationOptions
              {
                  options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/sign/Index");
              });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           

            app.UseRouting();
            app.UseAuthentication();    // аутентификация
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=sign}/{action=index}/{id?}");
            });
        }
    }
}
