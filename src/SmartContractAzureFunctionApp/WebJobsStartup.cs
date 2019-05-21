using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartContractAzureFunctionApp;
using SmartContractAzureFunctionApp.Services;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace SmartContractAzureFunctionApp
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder) => builder.AddDependencyInjection<ServiceProviderBuilder>();
    }

    internal class ServiceProviderBuilder : IServiceProviderBuilder
    {
        private readonly ILoggerFactory _loggerFactory;

        public ServiceProviderBuilder(ILoggerFactory loggerFactory) => _loggerFactory = loggerFactory;

        public IServiceProvider Build()
        {
            var services = new ServiceCollection();

            // Important: We need to call CreateFunctionUserCategory, otherwise our log entries might be filtered out.
            services.AddSingleton(_ => _loggerFactory.CreateLogger(LogCategories.CreateFunctionUserCategory("Common")));

            services.AddSingleton<ISmartContractService, SmartContractService>();

            return services.BuildServiceProvider();
        }
    }
}