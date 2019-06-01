using System.ComponentModel.DataAnnotations;

namespace Infrastructure.AzureTableStorage.Options
{
    public class AzureTableStorageOptions
    {
        [Required]
        [MinLength(0)]
        public string SASToken { get; set; }

        [Required]
        [MinLength(0)]
        public string BaseUri { get; set; }

        [Required]
        [MinLength(0)]
        public string SmartContractsTableName { get; set; }
    }
}