using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chatty.Data;
using Chatty.Models;
using Chatty.Services;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace Chatty
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /*services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();*/
            
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddMvc();

            //Add MailKit
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    //get options from sercets.json
                    Server = Configuration.GetSection("Email")["Server"],
                    Port = Convert.ToInt32(Configuration.GetSection("Email")["Port"]),
                    SenderName = Configuration.GetSection("Email")["Name"],
                    SenderEmail = Configuration.GetSection("Email")["Account"],

                    // can be optional with no authentication 
                    Account = Configuration.GetSection("Email")["Account"],
                    Password = Configuration.GetSection("Email")["Password"],
                    // enable ssl or tls
                    Security = true
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
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
