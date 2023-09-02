using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

using PS1_MIC_090_Core.Mapping;
using PS1_MIC_090_Core.Middlewares;
using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository;
using PS1_MIC_090_Core.Repository.Contracts;
using PS1_MIC_090_Core.Repository.Core;
using PS1_MIC_090_Core.Services;

using static Dapper.SqlMapper;

namespace PS1_MIC_090_Core.Models.Extensions
{
    public static class Extensions 
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            //DI
            //Generic
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

           
            //Repository
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IApplicationRepository), typeof(ApplicationRepository));
            //Services
            builder.Services.AddScoped(typeof(IApplicationService), typeof(ApplicationService));
            builder.Services.AddSingleton<AppService>();
            
            //Custom Events
            builder.Services.AddScoped<CustomCookieAuthenticationEvents>();


            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddDbContext<CoreDbContext>(options =>
            {
                options.UseSqlServer(AppConst.APP_SQL)
                .UseLazyLoadingProxies();
            });

           // builder.Services.AddIdentityCore<CoreUser>(options =>
           // {
           //     options.SignIn.RequireConfirmedAccount = false;
           //     options.Stores.MaxLengthForKeys = 128;
           //     options.SignIn.RequireConfirmedAccount = false;
           // })
           //.AddEntityFrameworkStores<CoreDbContext>();

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(o => { });


            builder.Services.AddIdentity<CoreUser, CoreRole>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<CoreDbContext>()
            .AddDefaultTokenProviders();

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


            //JSON Serializer
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                   Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        }
      
    }
   
}
