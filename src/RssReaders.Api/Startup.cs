using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RssReaders.Core.Repositories;
using RssReaders.Infrastructure.Mappers;
using RssReaders.Infrastructure.Repositories;
using RssReaders.Infrastructure.Services;
using RssReaders.Infrastructure.Settings;

namespace RssReaders.Api
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
            services.AddMvc()
                .AddNewtonsoftJson();
            services.AddAuthorization(x => x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
            services.AddScoped<IUserRepository, UserRepository> ();
            services.AddScoped<IAddressRepository,AddressRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddScoped<IParserService, ParserService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.Configure<JWTSettings>(Configuration.GetSection("jwt"));
            services.Configure<DatabaseSettings> (options => {
                options.ConnectionString = Configuration.GetSection ("MongoDb:ConnectionString").Value;
                options.Database = Configuration.GetSection ("MongoDb:Database").Value;
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting(routes =>
            {
                routes.MapControllers();
            });

            app.UseAuthorization();
        }
    }
}
