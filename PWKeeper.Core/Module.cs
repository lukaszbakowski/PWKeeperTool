
using Microsoft.Extensions.DependencyInjection;

namespace PWKeeper.Core
{
    public static class Module
    {
        public static IServiceCollection AddPWKeeperCore(this IServiceCollection service)
        {
            service.AddSingleton<StorageHandler>();
            return service;
        }
    }
}