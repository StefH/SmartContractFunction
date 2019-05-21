using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartContractAzureFunctionApp.Models;

namespace SmartContractAzureFunctionApp.Services
{
    public interface ISmartContractService
    {
        Task<SmartContractFunctionResponse> QueryFunctionAsync([NotNull] SmartContractFunctionRequest request);

        Task<SmartContractFunctionResponse> ExecuteFunctionAsync([NotNull] SmartContractFunctionRequest request);
    }
}
