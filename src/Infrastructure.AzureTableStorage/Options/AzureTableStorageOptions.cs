using System.ComponentModel.DataAnnotations;

namespace Infrastructure.AzureTableStorage.Options
{
    public class AzureTableStorageOptions
    {
        [Required]
        [MinLength(0)]
        public string ConnectionString { get; set; }
        
        [Required]
        [MinLength(0)]
        public string SmartContractsTableName { get; set; }
    }
}