using AzureFunctions.Common.Validation;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using SmartContractAzureFunctionApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartContractAzureFunctionApp.Services
{
    internal class SmartContractService : ISmartContractService
    {
        private static int TimeoutInSeconds = 60;

        public async Task<SmartContractFunctionResponse> QueryFunctionAsync(SmartContractFunctionRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var account = new Account(request.CallerPrivateKey);

            var web3 = new Web3Geth(account, request.Endpoint);

            var contract = web3.Eth.GetContract(request.ContractABI, request.ContractAddress);

            var function = contract.GetFunction(request.FunctionName);

            HexBigInteger estimatedGas = await function.EstimateGasAsync(request.FromAddress, null, null, request.FunctionParameters ?? new object[0]);

            return new SmartContractFunctionResponse
            {
                EstimatedGas = (ulong)estimatedGas.Value,
                Response = await function.CallAsync<object>(request.FromAddress, estimatedGas, null, request.FunctionParameters ?? new object[0])
            };
        }

        public async Task<SmartContractFunctionResponse> ExecuteFunctionAsync(SmartContractFunctionRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var account = new Account(request.CallerPrivateKey);

            var web3 = new Web3Geth(account, request.Endpoint);

            var contract = web3.Eth.GetContract(request.ContractABI, request.ContractAddress);

            var function = contract.GetFunction(request.FunctionName);

            HexBigInteger estimatedGas = await function.EstimateGasAsync(request.FromAddress, null, null, request.FunctionParameters ?? new object[0]);

            string transaction = await function.SendTransactionAsync(request.FromAddress, estimatedGas, null, null, request.FunctionParameters ?? new object[0]);
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(TimeoutInSeconds));
            while (receipt == null && !cts.IsCancellationRequested)
            {
                await Task.Delay(500, cts.Token);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);
            }

            return new SmartContractFunctionResponse
            {
                EstimatedGas = (ulong)estimatedGas.Value,
                Response = receipt != null,
                TransactionHash = receipt?.TransactionHash
            };
        }
    }
}
