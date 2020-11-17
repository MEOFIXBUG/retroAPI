using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using retroAPI.Commons.Extension;
using retroAPI.Commons.Provider;
using retroAPI.Commons.Repository;
using retroAPI.Commons.Repository.Interface;
using retroAPI.Models.DbModels;
using retroAPI.Modules.Boards;
using retroAPI.Modules.Boards.Interface;
using retroAPI.Modules.Jobs;
using retroAPI.Modules.Jobs.Interface;
using retroAPI.Modules.Users;
using retroAPI.Modules.Users.Interface;

namespace retroAPI
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();
            services.AddDbContext<heroku_4f2def07091704cContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LocalConnection"),
                mySqlOptionsAction: MySqlOptions =>
                {
                    MySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 4,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                        );
                }));
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //services.AddTokenAuthentication(Configuration);
            services.AddAuthentication()
      .AddGoogle(googleOptions =>
      {
          // Đọc thông tin Authentication:Google từ appsettings.json
          IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");

          // Thiết lập ClientID và ClientSecret để truy cập API google
          googleOptions.ClientId = googleAuthNSection["ClientId"];
          googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];

      });
            services.AddTransient<IRepository<Boards>, EntityRepository<Boards>>();
            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<IRepository<Users>, EntityRepository<Users>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRepository<Jobs>, EntityRepository<Jobs>>();
            services.AddTransient<IJobService, JobService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseCors(options =>
            {
                options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseMiddleware<TokenProviderMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
