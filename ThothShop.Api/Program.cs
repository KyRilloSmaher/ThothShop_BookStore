using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThothShop.Infrastructure.context;
using Microsoft.Extensions.Configuration;
using ThothShop.Infrastructure;
using ThothShop.Service;
using ThothShop.Core;
using ThothShop.Core.MiddleWares;
using Serilog;
using Stripe;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Connection To SQL SERVER
var connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddDbContext<ApplicationDBContext>(opt => { opt.UseSqlServer(connectionString); });

#endregion

#region Dependency injections
builder.Services.AddInfrastructureDependencies()
                .AddServciesDependencies()
                .AddCoreDependencies()
                .AddServiceRegisteration(builder.Configuration);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUrlHelper>(x =>
    x.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

StripeConfiguration.ApiKey = builder.Configuration["stripeSettings:SecretKey"];

#endregion

#region Localization
//builder.Services.AddControllersWithViews();
//builder.Services.AddLocalization(opt =>
//{
//    opt.ResourcesPath = "";
//});

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    List<CultureInfo> supportedCultures = new List<CultureInfo>
//    {
//            new CultureInfo("en-US"),
//            new CultureInfo("de-DE"),
//            new CultureInfo("fr-FR"),
//            new CultureInfo("ar-EG")
//    };

//    options.DefaultRequestCulture = new RequestCulture("en-US");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//});

#endregion
#region Serilog
Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddSerilog();
builder.Services.AddLogging();
#endregion
#region AllowCORS
var CORS = "_DefaultCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});

#endregion


var app = builder.Build();
#region Data Seeder 
 static async Task SeedDefaultUserAsync(UserManager<User> userManager)
{
    if (await userManager.FindByEmailAsync("Owner@thoth.com") == null)
    {
        var user = new User
        {
            UserName = "Owner@thoth.com",
            Email = "Owner@thoth.com",
            EmailConfirmed = true,
            Address= "A Town",
            PhoneNumber = "01102572716"
        };

        await userManager.CreateAsync(user, "Owner@123"); // Strong password required
        await userManager.AddToRoleAsync(user, "Owner");  // Optional: if using roles
    }
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();

    await SeedDefaultUserAsync(userManager);
}
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
//    var DepartmentManager = scope.ServiceProvider.GetRequiredService<IDepartmentRepository>();
//    await RoleSeeder.SeedAsync(roleManager);
//    await DepartmentSeeder.SeedAsync(DepartmentManager);
//}
#endregion
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

