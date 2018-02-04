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
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SAR Assistant API", Version = "v1" });
            });
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<ApplicationContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemory"));

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
            else if(env.IsEnvironment("Test"))
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
                var repository = app.ApplicationServices.GetService<IUserRepository>();
                InitializeDatabase(repository);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(null);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void InitializeDatabase(IUserRepository repo)
        {
            List<User> usersList = new List<User>(repo.GetAll());
            if (usersList.Count == 0)
            {
                usersList = GetTestUsers();

                usersList.ForEach(u => {
                    repo.Insert(u);
                });
            }
        }

        public static List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Matej",
                    LastName = "Janjic",
                    Address = new Location()
                    {
                        Id = 1,
                        Latitude = 53.4564M,
                        Longitude = 34.2324M,
                        Name = "Kuca",
                        Description = "Tu zivin"
                    },
                    Category = new Category
                    {
                        Id = 1,
                        Name = "Planinar"
                    },
                    Email = "a@b.com",
                    Password = "veoma dobra lozinka",
                    Role = new Role
                    {
                        Id = 1,
                        Name = "Admin"
                    },
                    AdditionalContactNumbers = "14124124",
                    AndroidPushId = "a1911421jasd2rj",
                    ContactNumber = "141412414",
                    Station = new Station
                    {
                        Id = 1,
                        Name = "Stanica ZG",
                        Location = new Location
                        {
                            Id = 2,
                            Name = "Lokacija stanice",
                            Description = "Tu se stanica nalazi",
                            Latitude = 53.131M,
                            Longitude = 12.1312M
                        }
                    },
                    Availiabilities = new List<Availability>
                    {
                        new Availability()
                        {
                            Id = 1,
                            Day = Days.Mon,
                            StartTime = new TimeSpan(12, 0, 0),
                            EndTime = new TimeSpan(15, 0, 0),
                            Location = new Location
                            {
                                Id = 3,
                                Name = "Poso",
                                Description = "Tu radin",
                                Latitude = 34.131M,
                                Longitude = 67.1312M
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Luka",
                    LastName = "Lazanja",
                    Address = new Location()
                    {
                        Id = 4,
                        Latitude = 23.4564M,
                        Longitude = 24.2324M,
                        Name = "Kuca",
                        Description = "Tu zivin"
                    },
                    Category = new Category
                    {
                        Id = 2,
                        Name = "Speleolog"
                    },
                    Email = "b@c.com",
                    Password = "veoma dobra lozinka",
                    Role = new Role
                    {
                        Id = 2,
                        Name = "Spasavatelj"
                    },
                    AdditionalContactNumbers = "14124124",
                    AndroidPushId = "a1911421jasd2rj",
                    ContactNumber = "141412414",
                    Station = new Station
                    {
                        Id = 2,
                        Name = "Stanica ZD",
                        Location = new Location
                        {
                            Id = 5,
                            Name = "Lokacija stanice u zd",
                            Description = "Tu se stanica nalazi",
                            Latitude = 23.131M,
                            Longitude = 32.1312M
                        }
                    },
                    Availiabilities = new List<Availability>
                    {
                        new Availability()
                        {
                            Id = 2,
                            Day = Days.Mon,
                            StartTime = new TimeSpan(12, 0, 0),
                            EndTime = new TimeSpan(15, 0, 0),
                            Location = new Location
                            {
                            Id = 6,
                                Name = "PosoZD",
                                Description = "Tu ne radin nista",
                                Latitude = 34.131M,
                                Longitude = 17.1312M
                            }
                        }
                    }
                }
            };

            return users;
        }
    }
}
