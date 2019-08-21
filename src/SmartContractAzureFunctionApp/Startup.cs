using AzureFunctions.Common;
using Infrastructure.AzureTableStorage.DependencyInjection;
using Infrastructure.AzureTableStorage.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartContractAzureFunctionApp.Options;
using SmartContractAzureFunctionApp.Services;
using YellowCounter.AzureFunctions.AspNetConfiguration;

// https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
// https://stackoverflow.com/questions/54876798/how-can-i-use-the-new-di-to-inject-an-ilogger-into-an-azure-function-using-iwebj
[assembly: FunctionsStartup(typeof(SmartContractAzureFunctionApp.Startup))]
namespace SmartContractAzureFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            #region YellowCounter.AzureFunctions.AspNetConfiguration
            //builder.UseAspNetConfiguration();
            //var configuration = builder.GetConfiguration();
            #endregion

            #region Stef
            var configBuilder = new ConfigurationBuilder();

            string scriptRoot = AzureFunctionUtils.GetAzureWebJobsScriptRoot();
            if (!string.IsNullOrEmpty(scriptRoot))
            {
                configBuilder.SetBasePath(scriptRoot).AddJsonFile("local.settings.json", optional: false, reloadOnChange: false);
            }
            configBuilder.AddEnvironmentVariables();

            var configuration = configBuilder.Build();
            #endregion

            // Add Services
            builder.Services.AddScoped<ISmartContractService, SmartContractService>();

            // Add 3rdParty Services
            builder.Services.AddAzureTableStorage();

            // Configure
            builder.Services.Configure<FunctionAppOptions>(configuration.GetSection("FunctionAppOptions"));
            builder.Services.Configure<AzureTableStorageOptions>(configuration.GetSection("AzureTableStorageOptions"));
        }
    }
}
