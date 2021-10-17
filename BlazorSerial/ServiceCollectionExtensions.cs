using Microsoft.Extensions.DependencyInjection;

namespace BlazorSerial
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorSerial(this IServiceCollection services) => services.AddSingleton<ISerialPort, SerialPort>();
    }
}