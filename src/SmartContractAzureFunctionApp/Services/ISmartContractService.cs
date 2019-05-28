using JetBrains.Annotations;
using SmartContractAzureFunctionApp.Models;
using System.Threading.Tasks;

namespace SmartContractAzureFunctionApp.Services
{
    public interface ISmartContractService
    {
        Task<SmartContractDeployResponse> DeployContractAsync([NotNull] SmartContractDeployRequest request);

        Task<SmartContractFunctionResponse> QueryFunctionAsync([NotNull] SmartContractFunctionRequest request);

        Task<SmartContractFunctionResponse> ExecuteFunctionAsync([NotNull] SmartContractFunctionRequest request);
    }
}
