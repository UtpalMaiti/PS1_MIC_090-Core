using BundlerMinifier.TagHelpers;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

using PS1_MIC_090_Core.Services;
using PS1_MIC_090_Core.Models.Extensions;
using PS1_MIC_090_Core.Middlewares;
using Microsoft.Net.Http.Headers;
using PS1_MIC_090_Core.Models.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;
using Serilog;

namespace PS1_MIC_090_Core
{
    class AppLogger
    {
        public void Invoke()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Disabled);
            });

            var logger = LoggerFactory.Create(config =>
            {
                config.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Disabled);
                config.AddConsole();
                config.AddDebug();


            }).CreateLogger(nameof(Program));

        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ;
            }


            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders().AddConsole().SetMinimumLevel(LogLevel.Information).Configure((o) =>
            {

            });
            builder.Host.UseSerilog((context, configuration)=>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                });
















            //builder.Services.AddRazorPages();

            builder.Services.AddMvc()
                .AddControllersAsServices();
            //builder.Services.AddAntiforgery();
            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            Extensions.ConfigureServices(builder);

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddBundles(options =>
            {
                options.AppendVersion = true;
            });
            // in configureServices() method 
            builder.Services.AddWebOptimizer();

            builder.Services.AddCors(options =>
                      options.AddPolicy(AppConst.CorsPolicy, builder =>
                      {
                          // Allow multiple HTTP methods  
                          builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
                            .WithHeaders(
                              HeaderNames.Accept,
                              HeaderNames.ContentType,
                              HeaderNames.Authorization)
                            .AllowCredentials()
                            .SetIsOriginAllowed(origin =>
                            {
                                if (string.IsNullOrWhiteSpace(origin)) return false;
                                if (origin.ToLower().StartsWith("http://localhost")) return true;
                                return false;
                            });
                      })
                    );


            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            // .AddCookie(options =>
            // {
            //     options.Cookie.Name = "Cookie_app";
            //     options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            //     options.SlidingExpiration = true;
            //     options.AccessDeniedPath = new PathString("/Home/Logon/");
            //     options.LoginPath = new PathString("/Home/Logon/");
            //     options.LogoutPath = new PathString("/Home/Index/");
            //     options.EventsType = typeof(CustomCookieAuthenticationEvents);
            //     options.Validate();
            //     //var defaultCallback = options.Events.OnRedirectToLogin;
            //     //options.Events.OnRedirectToLogin = context =>
            //     //{
            //     //    if (context.Request.Path.StartsWithSegments(new PathString("/member"), StringComparison.OrdinalIgnoreCase))
            //     //    {
            //     //        context.RedirectUri = new PathString("/Home/Logon/");
            //     //        context.Response.Redirect(context.RedirectUri);
            //     //    }
            //     //    else if (context.Request.Path.StartsWithSegments(new PathString("/consultant"), StringComparison.OrdinalIgnoreCase))
            //     //    {
            //     //        context.RedirectUri = new PathString("/Home/Logon/");
            //     //        context.Response.Redirect(context.RedirectUri);
            //     //    }
            //     //    return defaultCallback(context);
            //     //};
            //     //options.Validate();
            // });
            builder.Services.AddScoped<CustomCookieAuthenticationEvents>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRequestDecompression(options =>
            {
                options.DecompressionProviders.Add("custom", new CustomDecompressionProvider());
            });

            var app = builder.Build();

          
            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                app.UseRequestDecompression();
                // in Configure() method
                app.UseWebOptimizer();
            }
            else
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseCors(AppConst.CorsPolicy);

            app.UseHttpsRedirection();
            app.UseRequestCulture();
            app.UseMyCustomMiddleware();



            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                //MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always,

            };
            app.UseCookiePolicy(cookiePolicyOptions);



            //app.MapRazorPages();
            //app.MapDefaultControllerRoute();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            app.MapRazorPages();

            app.Run();
        }


    }
}