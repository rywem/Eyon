using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Eyon.DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Eyon.DataAccess.Data.Initializers;

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
            services.AddScoped<DataAccess.Data.Repository.IRepository.IUnitOfWork, DataAccess.Data.UnitOfWork>();
            services.AddScoped<DataAccess.Security.ISecurity.IRecipeSecurity, DataAccess.Security.RecipeSecurity>();
            services.AddScoped<DataAccess.Orchestrators.IOrchestrator.IRecipeOrchestrator, DataAccess.Orchestrators.RecipeOrchestrator>();
            services.AddScoped<DataAccess.DataCalls.IDataCall.IRecipeDataCall, DataAccess.DataCalls.RecipeDataCall>();
            services.AddScoped<DataAccess.DataCalls.IDataCall.IFeedDataCall, DataAccess.DataCalls.FeedDataCall>();
            services.AddScoped<DataAccess.Security.ISecurity.IFeedSecurity, DataAccess.Security.FeedSecurity>();
            services.AddScoped<DataAccess.Orchestrators.IOrchestrator.IFeedOrchestrator, DataAccess.Orchestrators.FeedOrchestrator>();

            services.AddScoped<DataAccess.Security.ISecurity.ICookbookSecurity, DataAccess.Security.CookbookSecurity>();
            services.AddScoped<DataAccess.Orchestrators.IOrchestrator.ICookbookOrchestrator, DataAccess.Orchestrators.CookbookOrchestrator>();
            //services.AddScoped<DataAccess.DataCalls.IDataCall.ICookbookDataCall, DataAccess.DataCalls.CookbookDataCall>();
            services.AddScoped<DataAccess.Images.IImageHelper, DataAccess.Images.ImageHelper>();
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
