using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestApp.DbContexts;
using QuestApp.Services;
using System.Linq;
using Radzen;
using Syncfusion.Blazor;
using QuestApp.Pages;

namespace QuestApp
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
            // Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzUzMkAzMTM5MmUzMzJlMzBGM3NvdU9HbWduN2c2S3NDSmlaTU9kQ0E5R1BlejhLUG1PQkd4TnE1cm5rPQ==");
            
            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddDbContext<QuestDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(DB.Constants.SERVER_TYPE)));
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "questauth";
                        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                    });
            services.AddHttpContextAccessor();
            services.AddSyncfusionBlazor();
            services.AddSignalR(e => { e.MaximumReceiveMessageSize = 65536; });

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();

            services.AddScoped<DBService>();
            services.AddScoped<SurveyService>();
            services.AddScoped<ResourcesService>();

            System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
            .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(ICultureService<>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedType => services.AddScoped(typeof(ICultureService<>), assignedType));
        }

        private RequestLocalizationOptions GetLocalizationOptions()
        {
            Extensions.CULTURES = Configuration.GetSection("Cultures")
                                .GetChildren().ToDictionary(x => x.Key, x => x.Value);
            var supportedCultures = Extensions.CULTURES.Keys.ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            return localizationOptions;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRequestLocalization(GetLocalizationOptions());
            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
