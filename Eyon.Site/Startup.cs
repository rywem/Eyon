using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Eyon.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Eyon.Core.Data.Initializers;

namespace Eyon.Site
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
            #region Scoped Dependency injection
            services.AddScoped<Core.Data.Repository.IRepository.IUnitOfWork, Core.Data.UnitOfWork>();
            services.AddScoped<Core.Security.ISecurity.IRecipeSecurity, Core.Security.RecipeSecurity>();
            services.AddScoped<Core.Orchestrators.IOrchestrator.IRecipeOrchestrator, Core.Orchestrators.RecipeOrchestrator>();
            services.AddScoped<Core.DataCalls.IDataCall.IRecipeDataCall, Core.DataCalls.RecipeDataCall>();
            services.AddScoped<Core.DataCalls.IDataCall.IFeedDataCall, Core.DataCalls.FeedDataCall>();
            services.AddScoped<Core.Security.ISecurity.IFeedSecurity, Core.Security.FeedSecurity>();
            services.AddScoped<Core.Orchestrators.IOrchestrator.IFeedOrchestrator, Core.Orchestrators.FeedOrchestrator>();

            services.AddScoped<Core.Security.ISecurity.ICookbookSecurity, Core.Security.CookbookSecurity>();
            services.AddScoped<Core.Orchestrators.IOrchestrator.ICookbookOrchestrator, Core.Orchestrators.CookbookOrchestrator>();
            //services.AddScoped<Core.DataCalls.IDataCall.ICookbookDataCall, Core.DataCalls.CookbookDataCall>();
            services.AddScoped<Core.Images.IImageHelper, Core.Images.ImageHelper>();
            #endregion
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

            });
            services.AddControllersWithViews()
                .AddNewtonsoftJson()
#if ( DEBUG )
                .AddRazorRuntimeCompilation()
#endif
                ;
            services.AddRazorPages();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add custom service
            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInit)
        {
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
            app.UseSession();
            app.UseRouting();

            dbInit.Initialize();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
