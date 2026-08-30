using Masar.Application;
using Masar.Application.Common.Interfaces;
using Masar.Infrastructure.Context;
using Masar.Infrastructure.Identity;
using Masar.Infrastructure.Services; // أو المكان المتواجد فيه TokenProvider
using MechanicShop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Masar.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MasarDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<MasarDbContext>());

// 2. Auth & Token Services (حل مشكلة ITokenProvider)
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// 3. Identity configuration
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

// 4. Identity Service
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "أدخل الـ Token الخاص بك هنا بهذا الشكل: Bearer {your_token}"
    });
});
// 5. Controllers & App Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IUser, CurrentUser>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();