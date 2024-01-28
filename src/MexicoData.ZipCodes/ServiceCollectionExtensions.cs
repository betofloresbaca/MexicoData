using Microsoft.Extensions.DependencyInjection;

namespace MexicoData.ZipCodes
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="IZipCodesService"/> in dependency injection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddZipCodesService(
            this IServiceCollection serviceCollection
        )
        {
            return serviceCollection.AddSingleton<IZipCodesService, ZipCodesService>();
        }
    }
}
