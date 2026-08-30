using Microsoft.Extensions.DependencyInjection;
using Masar.Application.Common.Behaviours;
using MechanicShop.Application.Common.Behaviours;
namespace Masar.Application;
using System.Reflection;
using FluentValidation;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
       
        // تسجيل MediatR والبحث عن كل الـ Handlers في طبقة الـ Application
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            //cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // تسجيل FluentValidation أو أي مكتبات أخرى هنا لاحقاً
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}