using JetBrains.Annotations;

namespace SmartContractAzureFunctionApp.Models
{
    [PublicAPI]
    public class SmartContractDeployResponse
    {
        public ulong GasEstimated { get; set; }

        public ulong GasUsed { get; set; }

        public string ContractAddress { get; set; }

        public string TransactionHash { get; set; }
    }
}