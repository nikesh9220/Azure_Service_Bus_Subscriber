using Microsoft.Extensions.DependencyInjection;
using Nikesh.Subscriber.Services;
using System;

namespace Nikesh.Subscriber
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            //SubscriberService.Run().GetAwaiter().GetResult();
            //RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<SubscriberService>().Run().GetAwaiter().GetResult();
            DisposeServices();
        }
        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<SubscriberService>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
