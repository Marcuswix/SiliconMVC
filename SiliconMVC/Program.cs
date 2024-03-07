using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.Middlewers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")), contextLifetime: ServiceLifetime.Transient);

            builder.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UserServer")), contextLifetime: ServiceLifetime.Transient);

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
            builder.Services.AddScoped<AddressServices>();
            builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<UserManager<UserEntity>>();
            builder.Services.AddScoped<SignInManager<UserEntity>>();

            builder.Services.AddDefaultIdentity<UserEntity>(x =>
             {
                 x.User.RequireUniqueEmail = true;
                 x.SignIn.RequireConfirmedAccount = false;
                 x.Password.RequiredLength = 8;
             }).AddEntityFrameworkStores<UserContext>();

            builder.Services.ConfigureApplicationCookie(x =>
            {
                //Detta f�rhindrar att n�gon kan l�sa ut cookieinformationen
                x.Cookie.HttpOnly = true;
                x.LoginPath = "/signin";
                x.LogoutPath = "/signout";
                x.AccessDeniedPath = "/denied";
                x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                //Anv�ndaren loggar ut automatisk efter 30 min...
                x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //Denna del g�r s� att ExpireTime nollst�ll s� for en sida laddas om...
                x.SlidingExpiration = true;
            });

            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseUserSessionValidation();
            //app.UseAuthentication(); //Vem �r du?
            app.UseAuthorization(); // Vad f�r du g�ra?

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
