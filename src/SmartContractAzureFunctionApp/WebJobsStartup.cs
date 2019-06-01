using AzureFunctions.Common;
using Infrastructure.AzureTableStorage.DependencyInjection;
using Infrastructure.AzureTableStorage.Options;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartContractAzureFunctionApp;
using SmartContractAzureFunctionApp.Services;
using System;
using SmartContractAzureFunctionApp.Options;
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

            var builder = new ConfigurationBuilder();

            string scriptRoot = AzureFunctionUtils.GetAzureWebJobsScriptRoot();
            if (!string.IsNullOrEmpty(scriptRoot))
            {
                builder.SetBasePath(scriptRoot).AddJsonFile("local.settings.json", optional: false, reloadOnChange: false);
            }
            builder.AddEnvironmentVariables();

            var configuration = builder.Build();

            // Important: We need to call CreateFunctionUserCategory, otherwise our log entries might be filtered out.
            services.AddSingleton(_ => _loggerFactory.CreateLogger(LogCategories.CreateFunctionUserCategory("Common")));

            services.AddSingleton<ISmartContractService, SmartContractService>();

            services.AddAzureTableStorage();

            // Configure
            services.Configure<FunctionAppOptions>(configuration.GetSection("FunctionAppOptions"));
            services.Configure<AzureTableStorageOptions>(configuration.GetSection("AzureTableStorageOptions"));

            return services.BuildServiceProvider();
        }
    }
}