using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

// Created with 'Nethereum-CodeGenerator' https://github.com/StefH/Nethereum-CodeGenerator by stef.heyenrath@mstack.nl (mStack B.V.)
// Based on abi-code-gen (https://github.com/Nethereum/abi-code-gen)
namespace DefaultNamespace
{
    public class SimpleStorageContractService : ISimpleStorageContractService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[],""name"":""getVersion"",""outputs"":[{""name"":""version"",""type"":""int256""},{""name"":""description"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""value"",""type"":""uint256""}],""name"":""setNumber"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""value"",""type"":""string""}],""name"":""setString"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getString"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""number1"",""type"":""uint256""},{""name"":""number2"",""type"":""uint256""}],""name"":""addNumbers"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getNumber"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""name"":""version"",""type"":""int256""},{""name"":""description"",""type"":""string""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";

        public static string ByteCode = "0x608060405234801561001057600080fd5b506040516106123803806106128339810180604052604081101561003357600080fd5b81516020830180519193928301929164010000000081111561005457600080fd5b8201602081018481111561006757600080fd5b815164010000000081118282018710171561008157600080fd5b5050600085905580519093506100a092506001915060208401906100a8565b505050610143565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106100e957805160ff1916838001178555610116565b82800160010185558215610116579182015b828111156101165782518255916020019190600101906100fb565b50610122929150610126565b5090565b61014091905b80821115610122576000815560010161012c565b90565b6104c0806101526000396000f3fe608060405234801561001057600080fd5b506004361061007e577c010000000000000000000000000000000000000000000000000000000060003504630d8e6e2c81146100835780633fb5c1cb1461010a5780637fcaf6661461012957806389ea642f146101cf578063ef9fc50b1461024c578063f2c9ecd814610281575b600080fd5b61008b610289565b6040518083815260200180602001828103825283818151815260200191508051906020019080838360005b838110156100ce5781810151838201526020016100b6565b50505050905090810190601f1680156100fb5780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b6101276004803603602081101561012057600080fd5b5035610328565b005b6101276004803603602081101561013f57600080fd5b81019060208101813564010000000081111561015a57600080fd5b82018360208201111561016c57600080fd5b8035906020019184600183028401116401000000008311171561018e57600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550610344945050505050565b6101d761035b565b6040805160208082528351818301528351919283929083019185019080838360005b838110156102115781810151838201526020016101f9565b50505050905090810190601f16801561023e5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b61026f6004803603604081101561026257600080fd5b50803590602001356103f2565b60408051918252519081900360200190f35b61026f6103f6565b6000805460018054604080516020600284861615610100026000190190941693909304601f81018490048402820184019092528181526060949392909183918301828280156103195780601f106102ee57610100808354040283529160200191610319565b820191906000526020600020905b8154815290600101906020018083116102fc57829003601f168201915b50505050509050915091509091565b600a81101561033b576001600255610341565b60028190555b50565b80516103579060039060208401906103fc565b5050565b60038054604080516020601f60026000196101006001881615020190951694909404938401819004810282018101909252828152606093909290918301828280156103e75780601f106103bc576101008083540402835291602001916103e7565b820191906000526020600020905b8154815290600101906020018083116103ca57829003601f168201915b505050505090505b90565b0190565b60025490565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061043d57805160ff191683800117855561046a565b8280016001018555821561046a579182015b8281111561046a57825182559160200191906001019061044f565b5061047692915061047a565b5090565b6103ef91905b80821115610476576000815560010161048056fea165627a7a72305820af39c0145ea4683cb6674df3f7bcb6dd04a63678d3e8c08c81c8e7d41e86180f0029";

        public static async Task<string> DeployContractAsync(Web3 web3, string addressFrom, BigInteger version, string description, CancellationTokenSource token = null, HexBigInteger gas = null)
        {
            if (gas == null)
            {
                BigInteger estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(ABI, ByteCode, addressFrom).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            var transactionReceipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(ABI, ByteCode, addressFrom, gas, token, version, description).ConfigureAwait(false);
            return transactionReceipt.ContractAddress;
        }

        private readonly Web3 _web3;
        private readonly Contract _contract;

        public SimpleStorageContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<ISimpleStorageContractService, Task<string>> func, int timeoutInSeconds)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSeconds));

            string transaction = await func(this);
            var receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction).ConfigureAwait(false);

            while (receipt == null && !cts.IsCancellationRequested)
            {
                await Task.Delay(500);
                receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction).ConfigureAwait(false);
            }

            return receipt;
        }

        public Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash)
        {
            return _web3.Eth.Blocks.GetBlockWithTransactionsByHash.SendRequestAsync(blockhash);
        }

        private Function GetFunctionGetVersion()
        {
            return _contract.GetFunction("getVersion");
        }

        private Function GetFunctionSetNumber()
        {
            return _contract.GetFunction("setNumber");
        }

        private Function GetFunctionSetString()
        {
            return _contract.GetFunction("setString");
        }

        private Function GetFunctionGetString()
        {
            return _contract.GetFunction("getString");
        }

        private Function GetFunctionAddNumbers()
        {
            return _contract.GetFunction("addNumbers");
        }

        private Function GetFunctionGetNumber()
        {
            return _contract.GetFunction("getNumber");
        }



        public Task<string> GetStringCallAsync()
        {
            var function = GetFunctionGetString();
            return function.CallAsync<string>();
        }

        public async Task<string> GetStringCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetString();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<BigInteger> AddNumbersCallAsync(BigInteger number1, BigInteger number2)
        {
            var function = GetFunctionAddNumbers();
            return function.CallAsync<BigInteger>(number1, number2);
        }

        public async Task<BigInteger> AddNumbersCallAsync(string addressFrom, BigInteger number1, BigInteger number2, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddNumbers();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, number1, number2).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, number1, number2).ConfigureAwait(false);
        }

        public Task<BigInteger> GetNumberCallAsync()
        {
            var function = GetFunctionGetNumber();
            return function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> GetNumberCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetNumber();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }


        public async Task<string> SetNumberAsync(string addressFrom, BigInteger value, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetNumber();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
        }

        public async Task<string> SetStringAsync(string addressFrom, string value, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetString();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
        }


        public Task<GetVersion> GetVersionCallAsync()
        {
            var function = GetFunctionGetVersion();
            return function.CallDeserializingToObjectAsync<GetVersion>();
        }

        public async Task<GetVersion> GetVersionCallAsync(string addressFrom,   HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetVersion();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallDeserializingToObjectAsync<GetVersion>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }



    }

    [FunctionOutput]
    public class GetVersion
    {
        [Parameter("int256", "version", 1)]
        public BigInteger Version { get; set; }

        [Parameter("string", "description", 2)]
        public string Description { get; set; }

    }



}
