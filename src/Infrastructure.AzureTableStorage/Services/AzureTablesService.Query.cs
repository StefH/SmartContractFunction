using AzureFunctions.Common.Validation;
using Infrastructure.AzureTableStorage.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WindowsAzure.Table.Extensions;

namespace Infrastructure.AzureTableStorage.Services
{
    internal partial class AzureTablesService
    {
        public async Task<SmartContractEntity> GetSmartContractAsync(string network, string address)
        {
            Guard.NotNullOrEmpty(network, nameof(network));
            Guard.NotNullOrEmpty(address, nameof(address));

            _logger.LogInformation("Querying Table '{table}' for Network '{network}' and Address '{address}'", _smartContractTable.Name, network, address);

            return await _smartContractTable.Set.FirstOrDefaultAsync(sc => sc.Network == network && sc.Address == address).ConfigureAwait(false);
        }
    }
}