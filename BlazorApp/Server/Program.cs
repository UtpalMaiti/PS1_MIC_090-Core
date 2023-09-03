using BlazorApp.Server.Data;
using BlazorApp.Server.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using BlazorApp.Server;
using BlazorApp.Server.Controllers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using BlazorApp.Server.Repository.Core;
using BlazorApp.Server.Services;
using BlazorApp.Server.Utility;

namespace BlazorApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration Configuration = builder.Configuration;

            IdentityModelEventSource.ShowPII = true;

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<AspNetUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentityServer()
                .AddApiAuthorization<AspNetUser, ApplicationDbContext>();

            builder.Services.AddAuthentication(o =>
            {
                //o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie(cfg => cfg.SlidingExpiration = true)
            .AddIdentityServerJwt()
            .AddJwtBearer(cfg =>
                {
                    cfg.Audience = "http://localhost:7009/";
                    cfg.Authority = "http://localhost:7009/";
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.Configuration = new OpenIdConnectConfiguration();
                });

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //      .RequireAuthenticatedUser()
            //      .Build();
            //});

            builder.Services.ConfigureCommonServices();
        

            // Add framework services.
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHealthChecks();
            builder.Services.AddSignalR(hubOptions =>
            {
                hubOptions.MaximumReceiveMessageSize = 32768;
            }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.WriteIndented = false;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            // in configureServices() method 
            builder.Services.AddWebOptimizer();
            builder.Services.AddBundling()
                    //.UseDefaults(Environment)
                    .UseNUglify();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRequestDecompression(options =>
            {
               
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestDecompression();
            // in Configure() method
            //app.UseWebOptimizer();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            };

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            //app.MapFallbackToFile("index.html");
            app.MapFallbackToPage("/BlazorIndex");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapDefaultControllerRoute();
            app.MapHealthChecks("/health", new HealthCheckOptions() { })
              .RequireAuthorization(new AuthorizeAttribute() { Roles = "admin", });
       
            app.MapNameSuffixEndpoints();
       
            


            app.Run();
        }
    }
}