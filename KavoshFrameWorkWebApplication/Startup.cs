using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog;
using KavoshFrameWorkWebApplication.AutoMapper;
using KavoshFrameWorkCore;
using Microsoft.EntityFrameworkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkData.Repositories;
using KavoshFrameWorkData.Repositories.Ldap;
using Newtonsoft.Json.Serialization;
using KavoshFrameWorkWebApplication.Helpers;
using System.Threading;
using System.Globalization;

namespace KavoshFrameWorkWebApplication
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


            services.AddDbContext<KavoshFrameWorkContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("KavoshFrameWorkData")));
            services.Configure<ConnectionConfig>(Configuration.GetSection("ConnectionStrings"));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<KavoshFrameWorkContext>()
            .AddDefaultTokenProviders();

            services.AddKendo();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            services
                .AddMvc()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDNTCaptcha(options => options.UseCookieStorageProvider());

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEncriptDescriptString, EncriptDescriptString>();
            services.AddScoped<IPingLdap, PingLdap>();
            services.AddScoped<IFindAllADUsers, FindAllADUsers>();
            services.AddScoped<IDepartmentQueries, DepartmentQueries>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped(typeof(ITreeRepository<>), typeof(TreeRepository<>));
            services.AddScoped(typeof(IEntityService), typeof(EntityService));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".TreeView.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(320);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            IMapper mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfileConfiguration());
            }).CreateMapper();

            services.AddSingleton(mapper);

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });
          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (httpContext, next) =>
            {
                //Get username  
                var username = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "anonymous";
                LogContext.PushProperty("User", username);

                //Get remote IP address  

                var ip = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                LogContext.PushProperty("IP", !String.IsNullOrWhiteSpace(ip) ? ip : "unknown");

                await next.Invoke();
            });

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
