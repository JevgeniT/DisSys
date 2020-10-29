#pragma warning disable 1591
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.App;
using Cache;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using DAL.App.NoSQL;
using Domain.Identity;
using EasyCaching.Core.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;

namespace WebApp
{
    public class Startup
    {
 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MySqlConnection")));
            
            services.Configure<MongoConnectionSettings>(
                Configuration.GetSection(nameof(MongoConnectionSettings)));

            services.AddSingleton<INoSqlConnectionSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoConnectionSettings>>().Value);
          
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            
            services.AddVersionedApiExplorer( options => options.GroupNameFormat = "'v'VVV" );
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
 
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IUserNameProvider, UserNameProvider>();
            services.AddScoped<IAppBLL, AppBLL>();
            services.AddScoped<MongoContext>();
            // services.AddScoped<IMongoAvailabilityRepository, MongoAvailabilityRepository>();
            services.AddMemoryCache();

            services.AddIdentity<AppUser, AppRole>()
                 .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.AddControllersWithViews();     
           services.AddRazorPages();
 
          services.AddCors(options =>
          {
              options.AddPolicy("CorsPolicy",
                  builder =>
                  {
                      builder.AllowAnyOrigin();
                      builder.AllowAnyHeader();
                      builder.AllowAnyMethod();
                  });
          });

          JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
          services
              .AddAuthentication()
              .AddCookie(options => { options.SlidingExpiration = true; })
              .AddJwtBearer(cfg =>
              {
                  cfg.RequireHttpsMetadata = false;
                  cfg.SaveToken = true;
                  cfg.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidIssuer = Configuration["JWT:Issuer"],
                      ValidAudience = Configuration["JWT:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                      ClockSkew = TimeSpan.Zero // remove delay of token when expire
                  };
              });
          services.AddApiVersioning(options =>
          {
              options.ReportApiVersions = true;
              // options.DefaultApiVersion = new ApiVersion(1,0);
              // options.AssumeDefaultVersionWhenUnspecified = false;
          });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // UpdateDatabase(app, env, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseResponseCaching();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
