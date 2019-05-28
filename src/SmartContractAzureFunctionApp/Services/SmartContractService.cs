using AzureFunctions.Common.Validation;
using Nethereum.Contracts;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
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

            var web3 = GetWeb3(request);

            var contract = web3.Eth.GetContract(request.ContractABI, request.ContractAddress);

            var function = contract.GetFunction(request.FunctionName);

            var functionParameters = request.FunctionParameters ?? new object[0];

            HexBigInteger estimatedGas = await function.EstimateGasAsync(request.FromAddress, null, null, functionParameters);

            var result = await function.CallAsync<object>(request.FromAddress, estimatedGas, null, functionParameters);

            return new SmartContractFunctionResponse
            {
                EstimatedGas = (ulong)estimatedGas.Value,
                Response = result
            };
        }

        public async Task<SmartContractFunctionResponse> ExecuteFunctionAsync(SmartContractFunctionRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var web3 = GetWeb3(request);

            var contract = web3.Eth.GetContract(request.ContractABI, request.ContractAddress);

            var function = contract.GetFunction(request.FunctionName);

            var functionParameters = request.FunctionParameters ?? new object[0];

            HexBigInteger estimatedGas = await function.EstimateGasAsync(request.FromAddress, null, null, functionParameters);

            var receipt = await SendTransactionAsync(request.FromAddress, function, functionParameters, estimatedGas, web3);

            return new SmartContractFunctionResponse
            {
                EstimatedGas = (ulong)estimatedGas.Value,
                Response = receipt != null,
                TransactionHash = receipt?.TransactionHash
            };
        }

        private static async Task<TransactionReceipt> SendTransactionAsync(string fromAddress, Function function, object[] functionParameters, HexBigInteger estimatedGas, Web3Geth web3)
        {
            string transaction = await function.SendTransactionAsync(fromAddress, estimatedGas, null, null, functionParameters);
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(TimeoutInSeconds));
            while (receipt == null && !cts.IsCancellationRequested)
            {
                await Task.Delay(500, cts.Token);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);
            }

            return receipt;
        }

        private static Web3Geth GetWeb3(SmartContractFunctionRequest request)
        {
            var account = new Account(request.CallerPrivateKey);

            return new Web3Geth(account, request.Endpoint);
        }
    }
}