using System.Threading.Tasks;
using SmartContractAzureFunctionApp.Models;

namespace SmartContractAzureFunctionApp.Services
{
    public interface ISmartContractService
    {
        Task<SmartContractFunctionResponse> QueryFunctionAsync(SmartContractFunctionRequest request);

        Task<SmartContractFunctionResponse> ExecuteFunctionAsync(SmartContractFunctionRequest request);
    }
}
