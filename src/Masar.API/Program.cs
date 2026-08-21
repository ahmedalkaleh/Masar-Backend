using Masar.Application;
using Masar.Application.Common.Interfaces;
using Masar.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// 1. إضافة اتصال قاعدة البيانات
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MasarDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<MasarDbContext>());

// 2. تسجيل الـ Controllers وخدمات Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
// ... يمكنك إضافة خدمات Application و MediatR هنا (مثل builder.Services.AddApplicationServices())

var app = builder.Build();

// 3. إعداد الـ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ توجيه الطلبات والتحقق
app.UseAuthorization();
app.MapControllers();

// 🚀 تشغيل السيرفر ومنعه من الإغلاق
app.Run();