using BundlerMinifier.TagHelpers;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

using PS1_MIC_090_Core.Services;
using PS1_MIC_090_Core.Models.Extensions;
using PS1_MIC_090_Core.Middlewares;

namespace PS1_MIC_090_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc().AddControllersAsServices();

            Extensions.ConfigureServices(builder);

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddBundles(options =>
            {
                options.AppendVersion = true;
            });
            // in configureServices() method 
            builder.Services.AddWebOptimizer();




            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                 options.SlidingExpiration = true;
                 options.AccessDeniedPath = "/logon/";
                 options.LoginPath = "/Logon";
                 options.LogoutPath = "/Logout";
                 options.EventsType = typeof(CustomCookieAuthenticationEvents);
             });


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRequestDecompression(options =>
            {
                options.DecompressionProviders.Add("custom", new CustomDecompressionProvider());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseRequestCulture();
            app.UseMyCustomMiddleware();
            app.UseRequestDecompression();

            // in Configure() method
            //app.UseWebOptimizer();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);

           

            //app.MapRazorPages();
            //app.MapDefaultControllerRoute();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

      
    }
}