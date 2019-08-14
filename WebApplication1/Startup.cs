using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace WebApplication1
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
            services
        .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
        .AddBasicAuthentication(
            options =>
            {
                options.Realm = "Insurance";
                options.Events = new BasicAuthenticationEvents
                {
                    OnValidatePrincipal = context =>
                    {
                        if ((context.UserName == "userName") && (context.Password == "password"))
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
                            };

                            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme));
                            context.Principal = principal;
                        }
                        else
                        {
                            // optional with following default.
                            // context.AuthenticationFailMessage = "Authentication failed."; 
                        }

                        return Task.CompletedTask;
                    }
                };
            });


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<CpeDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("InsuranceConnection")));
            services.AddScoped<Business.ApiCalls.ApiCalls>();
            services.AddScoped<Repository.Data.PolicyRepository>();
            services.AddScoped<Repository.Data.CustomerPolicyRepository>();
            services.AddScoped<Repository.Data.CustomerRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
