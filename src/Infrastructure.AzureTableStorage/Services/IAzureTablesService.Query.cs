using Infrastructure.AzureTableStorage.Models;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Infrastructure.AzureTableStorage.Services
{
    public interface IAzureTablesQueryService
    {
        Task<SmartContractEntity> GetSmartContractAsync([NotNull] string network, [NotNull] string address);
    }
}