using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Foodie.Web {
    public class Program {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.RegisterFoodieServices(builder.Configuration);
            builder.Services.RegisterAuthenticationServices(builder.Configuration);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
