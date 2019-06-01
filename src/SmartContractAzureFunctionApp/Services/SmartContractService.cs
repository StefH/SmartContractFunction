using AzureFunctions.Common.Extensions;
using AzureFunctions.Common.Validation;
using Infrastructure.AzureTableStorage.Models;
using Infrastructure.AzureTableStorage.Services;
using JetBrains.Annotations;
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
        private readonly IAzureTablesService _azure;

        public SmartContractService([NotNull] IAzureTablesService azure)
        {
            Guard.NotNull(azure, nameof(azure));

            _azure = azure;
        }

        public async Task<SmartContractDeployResponse> DeployContractAsync(SmartContractDeployRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var web3 = GetWeb3(request.CallerPrivateKey, request.Endpoint);

            object[] contractParameters = request.ContractParameters ?? new object[0];

            HexBigInteger gas = await web3.Eth.DeployContract.EstimateGasAsync(request.ContractABI, request.ContractByteCode, request.FromAddress, contractParameters);

            var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(
                request.ContractABI, request.ContractByteCode,
                request.FromAddress, gas, null, contractParameters);

            var entity = new SmartContractEntity
            {
                Network = GetNetwork(request.Endpoint),
                Address = receipt.ContractAddress,
                NetworkEndpoint = request.Endpoint,
                ABI = request.ContractABI,
                ByteCode = request.ContractByteCode,
                FromAddress = request.FromAddress,
                GasUsed = (long)receipt.GasUsed.Value,
                TransactionHash = receipt.TransactionHash
            };
            await _azure.StoreAsync(entity);

            return new SmartContractDeployResponse
            {
                GasEstimated = (ulong)gas.Value,
                GasUsed = (ulong)receipt.GasUsed.Value,
                ContractAddress = receipt.ContractAddress,
                TransactionHash = receipt.TransactionHash
            };
        }

        public async Task<SmartContractFunctionResponse> QueryFunctionAsync(SmartContractFunctionRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var web3 = GetWeb3(request.CallerPrivateKey, request.Endpoint);

            string abi = await GetContractABIAsync(request);

            var contract = web3.Eth.GetContract(abi, request.ContractAddress);

            var function = contract.GetFunction(request.FunctionName);

            object[] functionParameters = request.FunctionParameters ?? new object[0];

            HexBigInteger gas = await function.EstimateGasAsync(request.FromAddress, null, null, functionParameters);

            var result = await function.CallAsync<object>(request.FromAddress, gas, null, functionParameters);

            return new SmartContractFunctionResponse
            {
                GasEstimated = (ulong)gas.Value,
                Response = result
            };
        }

        public async Task<SmartContractFunctionResponse> ExecuteFunctionAsync(SmartContractFunctionRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var web3 = GetWeb3(request.CallerPrivateKey, request.Endpoint);

            string abi = await GetContractABIAsync(request);

            var contract = web3.Eth.GetContract(abi, request.ContractAddress);

            var function = contract.GetFunction(request.FunctionName);

            object[] functionParameters = request.FunctionParameters ?? new object[0];

            HexBigInteger gas = await function.EstimateGasAsync(request.FromAddress, null, null, functionParameters);

            var receipt = await SendTransactionAsync(web3, request.FromAddress, function, functionParameters, gas);

            return new SmartContractFunctionResponse
            {
                GasEstimated = (ulong)gas.Value,
                GasUsed = receipt != null ? (ulong?)receipt.GasUsed.Value : null,
                Response = receipt != null,
                TransactionHash = receipt?.TransactionHash
            };
        }

        private static async Task<TransactionReceipt> SendTransactionAsync(Web3Geth web3, string fromAddress, Function function, object[] functionParameters, HexBigInteger gas)
        {
            string transaction = await function.SendTransactionAsync(fromAddress, gas, null, null, functionParameters);
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(TimeoutInSeconds));
            while (receipt == null && !cts.IsCancellationRequested)
            {
                await Task.Delay(500, cts.Token);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);
            }

            return receipt;
        }

        private static Web3Geth GetWeb3(string privateKey, string endpoint)
        {
            var account = new Account(privateKey);

            return new Web3Geth(account, endpoint);
        }

        private static string GetNetwork(string endpoint)
        {
            if (endpoint.Contains("ropsten", StringComparison.OrdinalIgnoreCase))
            {
                return "Ropsten";
            }

            return "Unknown";
        }

        private async Task<string> GetContractABIAsync(SmartContractFunctionRequest request)
        {
            string network = GetNetwork(request.Endpoint);
            var entity = await _azure.GetSmartContractAsync(network, request.ContractAddress);
            return entity != null ? entity.ABI : request.ContractABI;
        }
    }
}