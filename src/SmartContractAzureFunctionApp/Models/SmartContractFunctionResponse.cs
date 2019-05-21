using Nethereum.Hex.HexTypes;

namespace SmartContractAzureFunctionApp.Models
{
    public class SmartContractFunctionResponse
    {
        public ulong EstimatedGas { get; set; }

        public object Response { get; set; }

        public string TransactionHash { get; set; }
    }
}