namespace SmartContractAzureFunctionApp.Models
{
    public class SmartContractFunctionRequest
    {
        public string Endpoint { get; set; }

        public string ContractABI { get; set; }

        public string ContractAddress { get; set; }

        public string CallerPrivateKey { get; set; }

        public string FunctionName { get; set; }

        public string FromAddress { get; set; }

        public object[] FunctionParameters { get; set; }
    }
}