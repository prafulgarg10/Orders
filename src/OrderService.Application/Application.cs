using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Application;

public class Application
{

}

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrdersService>();
        return services;
    }
}