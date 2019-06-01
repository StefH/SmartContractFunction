using Infrastructure.AzureTableStorage.Models;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Infrastructure.AzureTableStorage.Services
{
    public interface IAzureTablesStoreService
    {
        Task<bool> StoreAsync([NotNull] SmartContractEntity entity);
    }
}