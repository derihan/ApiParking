using ApiParking;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ApiParking.Data.Area;
using ApiParking.Data;
using ApiParking.Data.KArea;
using ApiParking.Data.Fees;
using ApiParking.Data.Slot;
using ApiParking.Data.User;
using ApiParking.Data.Cars;
using ApiParking.Data.History;
using ApiParking.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiParking
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
            //DB Context 
            services.AddDbContext<kparkingContext>(opt => opt.UseMySql(Configuration.GetConnectionString("ConnectionCommand")));
            services.AddDbContext<HistoryContext>(opt => opt.UseMySql(Configuration.GetConnectionString("ConnectionCommand")));
            services.AddDbContext<UserContext>(opt => opt.UseMySql(Configuration.GetConnectionString("ConnectionCommand")));

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(options => {
                options.AddPolicy("CorsPlicy", 
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build()
                );
            });

            var key = Configuration.GetValue<string>("secret_key");
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthentication(key));
            services.AddSignalR();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });
           

            //Service context
            services.AddScoped<IKAreaRepo, SqlKArea>();
            services.AddScoped<IAreaRepo, SqlArea>();
            services.AddScoped<IFeesRepocs, SqlFees>();
            services.AddScoped<ISlotRepo, SqlSlots>();
            services.AddScoped<IUserRepository, SqlUser>();
            services.AddScoped<ICarsRepository, SqlCars>();
            services.AddScoped<IHistoryRepocs, SqlHistory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ServerHub>("/signal");
                endpoints.MapControllers();
            });
        }
    }
}
