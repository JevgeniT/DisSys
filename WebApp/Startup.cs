using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF;
using Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<AppDbContext>(options =>
            //     options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MySqlConnection")));

            // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<AppDbContext>();
            //services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IUserNameProvider, UserNameProvider>();


            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();     
           services.AddRazorPages();

        //    services.AddDbContext<AppDbContext>(options =>
          //          options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));
          services.AddCors(options =>
          {
              options.AddPolicy("CorsAllowAll",
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
            
          services.AddVersionedApiExplorer( options => options.GroupNameFormat = "'v'VVV" );
          services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
          services.AddSwaggerGen();

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // UpdateDatabase(app, env, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
