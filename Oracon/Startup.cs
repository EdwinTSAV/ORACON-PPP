using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oracon.Maps;
using Oracon.Repositorio;
using Oracon.Servicios;

namespace Oracon
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
            services.AddControllersWithViews();
            services.AddDbContext<Oracon_Context>(
                o => o.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = "/user/login");

            services.AddTransient<IClaimService, ClaimService>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IHomeRepo, HomeRepo>();
            services.AddTransient<IAdminRepo, AdminRepo>();
            services.AddTransient<IDocenteRepo, DocenteRepo>();
            services.AddTransient<IAjustesRepo, AjustesRepo>();
            services.AddTransient<ICursoRepo, CursoRepo>();
            services.AddTransient<IAprendizajeRepo, AprendizajeRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            });
        }
    }
}
