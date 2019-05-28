using JetBrains.Annotations;

namespace SmartContractAzureFunctionApp.Models
{
    [PublicAPI]
    public class SmartContractDeployRequest
    {
        public string Endpoint { get; set; }

        public string ContractABI { get; set; }

        public string ContractByteCode { get; set; }

        public object[] ContractParameters { get; set; }

        public string FromAddress { get; set; }

        public string CallerPrivateKey { get; set; }
    }
}