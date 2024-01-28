using Microsoft.Extensions.DependencyInjection;

namespace MexicoData.ZipCodes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZipCodesService(
            this IServiceCollection serviceCollection
        )
        {
            return serviceCollection.AddSingleton<IZipCodesService, ZipCodesService>();
        }
    }
}
