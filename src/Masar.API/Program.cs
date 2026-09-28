using Masar.API.Services;
using Masar.Application;
using Masar.Application.Common.Interfaces;
using Masar.Infrastructure.Context;
using Masar.Infrastructure.Identity;
using Masar.Infrastructure.Services;
using MechanicShop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. Database
// ============================================================

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MasarDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<MasarDbContext>());


// ============================================================
// 2. Authentication / Token Services
// ============================================================

builder.Services.AddScoped<ITokenProvider, TokenProvider>();


// ============================================================
// 3. ASP.NET Identity
// ============================================================

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<MasarDbContext>()
.AddDefaultTokenProviders();


// ============================================================
// 4. Identity Service
// ============================================================

builder.Services.AddScoped<IIdentityService, IdentityService>();


// ============================================================
// 5. Controllers
// ============================================================

builder.Services.AddControllers();


// ============================================================
// 6. Application Services
// ============================================================

builder.Services.AddApplicationServices();


// ============================================================
// 7. Current User
// ============================================================

builder.Services.AddScoped<IUser, CurrentUser>();


// ============================================================
// 8. Trip Collision Checker
// ============================================================

builder.Services.AddScoped<ITripCollisionChecker, TripCollisionChecker>();


// ============================================================
// 9. API Explorer
// ============================================================

builder.Services.AddEndpointsApiExplorer();


// ============================================================
// 10. Swagger / OpenAPI
// ============================================================

builder.Services.AddSwaggerGen(options =>
{
    // --------------------------------------------------------
    // JWT Bearer Authentication
    // --------------------------------------------------------

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "أدخل الـ Token الخاص بك بهذا الشكل: Bearer {your_token}"
    });


    // --------------------------------------------------------
    // Masar.API XML Documentation
    // --------------------------------------------------------

    var apiXmlFile =
        $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var apiXmlPath =
        Path.Combine(
            AppContext.BaseDirectory,
            apiXmlFile);

    if (File.Exists(apiXmlPath))
    {
        options.IncludeXmlComments(apiXmlPath);
    }


    // --------------------------------------------------------
    // Masar.Application XML Documentation
    // --------------------------------------------------------

    var applicationXmlPath =
        Path.Combine(
            AppContext.BaseDirectory,
            "Masar.Application.xml");

    if (File.Exists(applicationXmlPath))
    {
        options.IncludeXmlComments(applicationXmlPath);
    }
});


// ============================================================
// 11. Build Application
// ============================================================

var app = builder.Build();


// ============================================================
// 12. Swagger Middleware
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// ============================================================
// 13. HTTPS
// ============================================================

app.UseHttpsRedirection();


// ============================================================
// 14. Authentication & Authorization
// ============================================================

app.UseAuthentication();

app.UseAuthorization();


// ============================================================
// 15. Controllers
// ============================================================

app.MapControllers();


// ============================================================
// 16. Run
// ============================================================

app.Run();