using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Queries.GetOrder;
using OrderService.Application.Queries.GetOrders;

namespace OrderService.Application;

public static class ApplicationDI
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderHandler>();
        services.AddScoped<GetOrderHandler>();
        services.AddScoped<GetOrdersHandler>();
        return services;
    }
}