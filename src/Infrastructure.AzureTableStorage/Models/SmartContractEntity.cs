using WindowsAzure.Table.Attributes;

namespace Infrastructure.AzureTableStorage.Models
{
    public class SmartContractEntity
    {
        [PartitionKey]
        public string Network { get; set; }

        [RowKey]
        public string Address { get; set; }

        public string NetworkEndpoint { get; set; }

        public string ABI { get; set; }

        public string ByteCode { get; set; }

        public string FromAddress { get; set; }

        public long GasUsed { get; set; }

        public string TransactionHash { get; set; }
    }
}