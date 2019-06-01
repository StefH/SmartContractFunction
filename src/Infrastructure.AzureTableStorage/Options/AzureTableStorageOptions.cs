using System.ComponentModel.DataAnnotations;

namespace Infrastructure.AzureTableStorage.Options
{
    public class AzureTableStorageOptions
    {
        public string ConnectionString { get; set; }

        public string SASToken { get; set; }

        public string BaseUri { get; set; }

        [Required]
        [MinLength(0)]
        public string SmartContractsTableName { get; set; }
    }
}