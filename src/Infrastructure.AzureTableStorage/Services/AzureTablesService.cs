﻿using System;
using AzureFunctions.Common.Validation;
using Infrastructure.AzureTableStorage.Models;
using Infrastructure.AzureTableStorage.Options;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WindowsAzure.Table;

namespace Infrastructure.AzureTableStorage.Services
{
    internal partial class AzureTablesService : IAzureTablesService
    {
        private readonly ILogger<AzureTablesService> _logger;

        private readonly (string Name, ITableSet<SmartContractEntity> Set) _smartContractTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTablesService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public AzureTablesService([NotNull] IOptions<AzureTableStorageOptions> options, [NotNull] ILogger<AzureTablesService> logger)
        {
            Guard.NotNull(options, nameof(options));
            Guard.NotNull(logger, nameof(logger));

            _logger = logger;

            CloudTableClient client;
            if (!string.IsNullOrEmpty(options.Value.ConnectionString))
            {
                _logger.LogInformation("AzureTablesService : using ConnectionString");
                client = CloudStorageAccount.Parse(options.Value.ConnectionString).CreateCloudTableClient();
            }
            else if (!string.IsNullOrEmpty(options.Value.SASToken) && !string.IsNullOrEmpty(options.Value.BaseUri))
            {
                _logger.LogInformation("AzureTablesService : using BaseUri and SASToken");
                var storageCredentials = new StorageCredentials(options.Value.SASToken);

                // Create CloudTableClient using the BaseUri and the StorageCredentials
                client = new CloudTableClient(new Uri(options.Value.BaseUri), storageCredentials);
            }
            else
            {
                throw new ArgumentException("AzureTableStorageOptions is missing 'ConnectionString' or 'BaseUri' + 'SASToken'");
            }

            // Create table set(s)
            _smartContractTable = (options.Value.SmartContractsTableName, new TableSet<SmartContractEntity>(client, options.Value.SmartContractsTableName));
        }
    }
}