using AzureFunctions.Common.Validation;
using Infrastructure.AzureTableStorage.Models;
using Infrastructure.AzureTableStorage.Options;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using WindowsAzure.Table;

namespace Infrastructure.AzureTableStorage.Services
{
    internal partial class AzureTablesService : IAzureTablesService
    {
        private readonly ILogger _logger;

        private readonly (string Name, ITableSet<SmartContractEntity> Set) _smartContractTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTablesService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public AzureTablesService([NotNull] IOptions<AzureTableStorageOptions> options, [NotNull] ILogger logger)
        {
            Guard.NotNull(options, nameof(options));
            Guard.NotNull(logger, nameof(logger));

            _logger = logger;

            var storageCredentials = new StorageCredentials(options.Value.SASToken);

            // Create CloudTableClient using the BaseUri and the StorageCredentials
            var client = new CloudTableClient(new Uri(options.Value.BaseUri), storageCredentials);

            // Create table set(s)
            _smartContractTable = (options.Value.SmartContractsTableName, new TableSet<SmartContractEntity>(client, options.Value.SmartContractsTableName));
        }
    }
}