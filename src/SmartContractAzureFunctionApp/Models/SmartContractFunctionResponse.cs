using JetBrains.Annotations;

namespace SmartContractAzureFunctionApp.Models
{
    [PublicAPI]
    public class SmartContractFunctionResponse
    {
        public ulong EstimatedGas { get; set; }

        public object Response { get; set; }

        public string TransactionHash { get; set; }
    }
}