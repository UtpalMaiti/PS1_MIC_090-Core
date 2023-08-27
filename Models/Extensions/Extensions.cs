using AutoMapper;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

using PS1_MIC_090_Core.Mapping;
using PS1_MIC_090_Core.Middlewares;
using PS1_MIC_090_Core.Repository;
using PS1_MIC_090_Core.Repository.Contracts;
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
