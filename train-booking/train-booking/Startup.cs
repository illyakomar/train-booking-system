using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using train_booking.Models;   
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Hosting;
using train_booking.Data;
using Microsoft.AspNetCore.Identity;
using train_booking.Services.Interfaces;
using train_booking.Services.Repositories;
using train_booking.Services;
using Microsoft.AspNetCore.Http;

namespace train_booking
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TrainBookingContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
        

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<TrainBookingContext>()
            .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IDispatchersRepository, DispatchersRepository>();
            services.AddScoped<ITrainDriversRepository, TrainDriversRepository>();
            services.AddScoped<ITrainsRepository, TrainsRepository>();
            services.AddScoped<IWagonsRepository, WagonsRepository>();
            services.AddScoped<IRoutesRepository, RoutesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TrainBookingContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            InitializeIdentity(serviceProvider);
        }

        private void InitializeIdentity(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            Task<IdentityResult> roleResultAdministrator;
            Task<IdentityResult> roleResultDispatcher;
            Task<IdentityResult> roleResultTrainDriver;
            Task<IdentityResult> roleResultPassenger;

            var admin = Configuration.GetSection("Administrator");

            string adminEmail = admin.GetValue<string>("email");
            string adminPassword = admin.GetValue<string>("password");

            //Check that there is an Administrator role and create if not
            Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResultAdministrator = roleManager.CreateAsync(new IdentityRole("Administrator"));
                roleResultAdministrator.Wait();
            }

            Task<bool> hasDispatcherRole = roleManager.RoleExistsAsync("Dispatcher");
            hasDispatcherRole.Wait();

            if (!hasDispatcherRole.Result)
            {
                roleResultDispatcher = roleManager.CreateAsync(new IdentityRole("Dispatcher"));
                roleResultDispatcher.Wait();
            }

            Task<bool> hasTrainDriverRole = roleManager.RoleExistsAsync("TrainDriver");
            hasTrainDriverRole.Wait();

            if (!hasTrainDriverRole.Result)
            {
                roleResultTrainDriver = roleManager.CreateAsync(new IdentityRole("TrainDriver"));
                roleResultTrainDriver.Wait();
            }

            Task<bool> hasPassengerRole = roleManager.RoleExistsAsync("Passenger");
            hasPassengerRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResultPassenger = roleManager.CreateAsync(new IdentityRole("Passenger"));
                roleResultPassenger.Wait();
            }

            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            Task<User> adminUser = userManager.FindByEmailAsync(adminEmail);
            adminUser.Wait();

            if (adminUser.Result == null)
            {
                User administrator = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, adminPassword);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
                    newUserRole.Wait();
                }
            }
        }
    }
}
