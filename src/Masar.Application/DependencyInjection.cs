using Microsoft.Extensions.DependencyInjection;

namespace Masar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // تسجيل MediatR والبحث عن كل الـ Handlers في طبقة الـ Application
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // تسجيل FluentValidation أو أي مكتبات أخرى هنا لاحقاً

        return services;
    }
}