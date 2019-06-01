using Infrastructure.AzureTableStorage.Services;

namespace Infrastructure.AzureTableStorage.Factories
{
    public interface IAzureTablesServiceFactory
    {
        IAzureTablesService Create();
    }
}