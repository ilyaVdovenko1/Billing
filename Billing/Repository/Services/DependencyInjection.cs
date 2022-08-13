using Billing.Repository.Interfaces;

namespace Billing.Repository.Services;

public static class DependencyInjection
{
    public static IRepositoryService AddRepository(this IServiceCollection serviceCollection, FileStream fileStream)
    {
        var service = new DefaultAppService(fileStream);
        serviceCollection.AddSingleton<IRepositoryService>(service);
        return service;
    }
}

