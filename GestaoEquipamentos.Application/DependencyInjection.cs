using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoEquipamentos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IEquipmentHistoryService, EquipmentHistoryService>();

        return services;
    }
}
