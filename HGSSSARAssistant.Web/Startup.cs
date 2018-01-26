using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HGSSSARAssistant.DAL.EF;
using HGSSSARAssistant.DAL;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.Core;
using Microsoft.AspNetCore.Identity;
using HGSSSARAssistant.Web.Services;

namespace HGSSSARAssistant.Web
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
            services.AddMvc();

            var connection = Configuration.GetConnectionString("Elephant");
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStationRepository, StationRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IActionTypeRepository, ActionTypeRepository>();
            services.AddTransient<IAvailabilityRepository, AvailabilityRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IExpertiseRepository, ExpertiseRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IMessageTemplateRepository, MessageTemplateRepository>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IActionNotifier, ActionPushNotifier>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
