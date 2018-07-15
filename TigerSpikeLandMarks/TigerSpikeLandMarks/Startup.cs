using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TigerSpikeLandMarks.DBContexts;
using Microsoft.EntityFrameworkCore;
using TigerSpikeLandMarks.Managers.UserManager;
using TigerSpikeLandMarks.Managers.LandMarkManager;
using TigerSpikeLandMarks.Repositories;

namespace TigerSpikeLandMarks
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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials());
            });
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            //services.AddDbContext<SQLDBContext>(x => x.UseInMemoryDatabase("TestDb"));
            services.Configure<AppSettings>(Configuration.GetSection("sqlServer"));
            services.AddDbContext<SQLDBContext>((sp, options) => options.UseSqlServer(sp.GetRequiredService<IOptions<AppSettings>>().Value.ConnectionString));


            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ILandMarkAndNotesManager, LandMarAndkNotesManager>();
            services.AddTransient<IDataRepository, DataRepository>();

            
            authenticationService(services);
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(
        options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    );
            app.UseAuthentication();
            app.UseMvc();
        }

        #region Validate Token
        private void authenticationService(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "landmarks.tigerspike.com",
                    ValidAudience = "landmarks.tigerspike.com",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("AFC2343FDFASXC45234ADFDSFVSDF1"))
                };
            });
        }
        #endregion
    }
}
