using DtoNetProject.Repositories;
using DtoNetProject.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetLoveMatcher_Backend.Data;
using PetLoveMatcher_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


//###################################
//#         BAZA PODATAKA           #
//###################################
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySqlDatabase"), new MySqlServerVersion(new Version(8, 0, 32))));
//###################################



//#####################################
//#         SERVISI I REPOZITORIJUMI  #
//#####################################
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<UserService>();
//###################################



//###################################
//#         AUTORIZACIJA            #
//###################################
builder.Services.AddIdentity<User, IdentityRole>(
options =>
{
    // Configure Identity options
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = false;
}
)
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
//###################################


//###########################
//#         CORS            #
//###########################
var MyAllowSpecificOrigins = "Angular-Frontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:4200")
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .AllowAnyMethod();
                      });
});
//###################################




//###########################
//#         ANTIFORGERY     #
//###########################
//builder.Services.AddAntiforgery(options =>
//{
//    //Set Cookie properties using CookieBuilder properties†.
//    options.HeaderName = "XSRF-TOKEN";
//    options.SuppressXFrameOptionsHeader = false;

//    options.Cookie.SameSite = SameSiteMode.None;
//    options.Cookie.Path = "/";
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//});
//###################################


var app = builder.Build();


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: ""
);


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();

    if (!context.Users.Any())
    {
        context.Database.Migrate();

        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        DbInitializer.SeedData(context, userManager, roleManager).Wait();
    }
}

app.Run();
