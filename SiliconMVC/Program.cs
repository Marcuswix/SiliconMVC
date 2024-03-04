using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace SiliconMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
            builder.Services.AddDefaultIdentity<UserEntity>(x =>
             {
                 x.User.RequireUniqueEmail = true;
                 x.SignIn.RequireConfirmedAccount = false;
                 x.Password.RequiredLength = 8;
             }).AddEntityFrameworkStores<DataContext>();

            builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<AddressEntity>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserEntity>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<AddressServices>();
            builder.Services.AddScoped<FeatureRepository>();
            builder.Services.AddScoped<FeatureItemRepository>();
            builder.Services.AddScoped<FeatureService>();
            builder.Services.AddScoped<IntegrateRepository>();
            builder.Services.AddScoped<IntegrateItemRepository>();
            builder.Services.AddScoped<IntegrateService>();
            builder.Services.AddScoped<AccountServices>();

            builder.Services.AddAuthentication("AuthCookie").AddCookie("AuthCookie", x =>
            {
            x.LoginPath = "/signin";
            x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });


            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            //app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication(); //Vem är du?
            app.UseAuthorization(); // Vad får du göra?

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
