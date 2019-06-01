using System;
using AzureFunctions.Common.Validation;
using Infrastructure.AzureTableStorage.Options;
using Infrastructure.AzureTableStorage.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Infrastructure.AzureTableStorage.Factories
{
    internal class AzureTablesServiceFactory : IAzureTablesServiceFactory
    {
        private readonly IOptions<AzureTableStorageOptions> _options;

        public AzureTablesServiceFactory([NotNull] IOptions<AzureTableStorageOptions> options)
        {
            Guard.NotNull(options, nameof(options));
            _options = options;
        }

        public IAzureTablesService Create()
        {
            CloudTableClient client;
            if (!string.IsNullOrEmpty(_options.Value.ConnectionString))
            {
                return client = CloudStorageAccount.Parse(_options.Value.ConnectionString).CreateCloudTableClient();
            }

            var storageCredentials = new StorageCredentials(options.Value.SASToken);

            // Create CloudTableClient using the BaseUri and the StorageCredentials
            var client = new CloudTableClient(new Uri(options.Value.BaseUri), storageCredentials);
            throw new System.NotImplementedException();
        }
    }
}
