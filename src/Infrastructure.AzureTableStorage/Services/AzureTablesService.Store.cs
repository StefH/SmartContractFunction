using AzureFunctions.Common.Validation;
using Infrastructure.AzureTableStorage.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.AzureTableStorage.Services
{
    internal partial class AzureTablesService
    {
        public async Task<bool> StoreAsync(SmartContractEntity entity)
        {
            Guard.NotNull(entity, nameof(entity));

            _logger.LogInformation("Inserting SmartContractEntity with PartitionKey '{PartitionKey}' and RowKey '{RowKey}' into Azure Table '{Table}'",
                entity.Network, entity.Address, _smartContractTable.Name);

            return await _smartContractTable.Set.AddAsync(entity).ConfigureAwait(false) != null;
        }
    }
}