using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PLFApp.Server.EntityFrameworkCore;
using PLFApp.Server.WebApi.Middleware;
using System.Text;

namespace PLFApp.Server.WebApi
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
            services.AddDbContext<PLFAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.InitializeDI();
            services.AddMvc();
            services.AddAuthentication((options) =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters() {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidIssuer = Configuration.GetSection("JWTBearer").GetValue<string>("Issuer"),
                    ValidAudience = "api",
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWTBearer").GetValue<string>("ClientSeret")))
                };
                options.RequireHttpsMetadata = false;
                //options.Audience = "api";//api范围 
                //options.Authority = Configuration["IdentityServer-Authority"];//IdentityServer地址  
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            var AllowOriginSites = Configuration["AllowOriginSiteNames"];
            if (!string.IsNullOrWhiteSpace(AllowOriginSites))
            {
                app.UseCrossSiteAccess(new CrossSiteAccessOptions()
                {
                    AllowOriginSites = AllowOriginSites.Split(',')
                });
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
