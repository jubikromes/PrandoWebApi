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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PrandoWebServices.Auth.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PrandoWebServices.Utilities.Helpers;
using PrandoWebServices.Auth.TokenFactory;
using AutoMapper;
using PrandoWebServices.Data.Identity;
using Microsoft.AspNetCore.Identity;
using PrandoWebServices.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PrandoWebServices.Filters.Auth;
using PrandoWebServices.Data.Abstractions;
using System.Reflection;
using PrandoWebServices.Repositories;
using PrandoWebServices.Services.UserService;

namespace PrandoWebServices
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PrandoDbContext>(options =>
                 options.UseSqlServer(connectionString));

            services.AddSingleton<IJwtFactory, JwtFactory>();
            var jwtsectionOptions =  Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options => {
                options.Issuer = jwtsectionOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtsectionOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, ValidIssuer = jwtsectionOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true, ValidAudience = jwtsectionOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateIssuerSigningKey = true, IssuerSigningKey = _signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options => {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions => {
                configureOptions.ClaimsIssuer = jwtsectionOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options => {
                options.AddPolicy(Constants.Strings.JwtClaimIdentifiers.Pol_ApiAccess_Key, policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.Pol_ApiAccess));
                options.AddPolicy(Constants.Strings.JwtClaimIdentifiers.Pol_ApproveVehicle_Key, policy => policy.RequireClaim(Constants.Strings.JwtClaims.Pol_ApproveVehicle));
            });
            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<PrandoDbContext>().AddDefaultTokenProviders();

            //register dependencies
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAccountService, AccountService>();
            //register dependencies
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options => options.AddPolicy("AllowAllHeaders", bui => bui.AllowAnyOrigin()
                .AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            loggerFactory.AddConsole();
            app.UseAuthentication();
            app.UseCors("AllowAllHeaders");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
