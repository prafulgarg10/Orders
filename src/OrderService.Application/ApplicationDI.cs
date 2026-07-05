using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.Application;

public static class ApplicationDI
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderHandler>();
        return services;
    }
}