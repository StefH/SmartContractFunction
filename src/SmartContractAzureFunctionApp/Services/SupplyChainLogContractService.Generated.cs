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
    public class SupplyChainLogContractService : ISupplyChainLogContractService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[{""name"":""index"",""type"":""uint256""}],""name"":""getOrderIdAtIndex"",""outputs"":[{""name"":""orderId"",""type"":""bytes16""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""orderId"",""type"":""bytes16""}],""name"":""getOrderById"",""outputs"":[{""name"":""id"",""type"":""bytes16""},{""name"":""name"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""name"",""type"":""string""}],""name"":""addOrder"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""id"",""type"":""bytes16""}],""name"":""isExistingOrder"",""outputs"":[{""name"":""existing"",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getOrderCount"",""outputs"":[{""name"":""count"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""id"",""type"":""bytes16""},{""indexed"":false,""name"":""name"",""type"":""string""}],""name"":""AddOrderEvent"",""type"":""event""}]";

        public static string ByteCode = "0x608060405234801561001057600080fd5b5060008054600160a060020a03191633179055610815806100326000396000f3fe608060405234801561001057600080fd5b5060043610610073577c01000000000000000000000000000000000000000000000000000000006000350463024303b181146100785780631fc5a75a146100bb5780632cca04df1461017d5780634b506d5c146102255780638d0a5fbb14610269575b600080fd5b6100956004803603602081101561008e57600080fd5b5035610283565b604080516fffffffffffffffffffffffffffffffff199092168252519081900360200190f35b6100eb600480360360208110156100d157600080fd5b50356fffffffffffffffffffffffffffffffff1916610327565b604080516fffffffffffffffffffffffffffffffff198416815260208082018381528451938301939093528351919291606084019185019080838360005b83811015610141578181015183820152602001610129565b50505050905090810190601f16801561016e5780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b6102236004803603602081101561019357600080fd5b8101906020810181356401000000008111156101ae57600080fd5b8201836020820111156101c057600080fd5b803590602001918460018302840111640100000000831117156101e257600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550610415945050505050565b005b6102556004803603602081101561023b57600080fd5b50356fffffffffffffffffffffffffffffffff1916610693565b604080519115158252519081900360200190f35b610271610727565b60408051918252519081900360200190f35b6000808210156102de576040517f08c379a00000000000000000000000000000000000000000000000000000000081526004018080602001828103825260238152602001806107c76023913960400191505060405180910390fd5b60028054839081106102ec57fe5b90600052602060002090600291828204019190066010029054906101000a90047001000000000000000000000000000000000290505b919050565b6000606061033483610693565b151561033c57fe5b6fffffffffffffffffffffffffffffffff198316600090815260016020818152604092839020805481840180548651600261010097831615979097026000190190911695909504601f810185900485028601850190965285855291947001000000000000000000000000000000009091029391928391908301828280156104045780601f106103d957610100808354040283529160200191610404565b820191906000526020600020905b8154815290600101906020018083116103e757829003601f168201915b505050505090509250925050915091565b805160001061048557604080517f08c379a000000000000000000000000000000000000000000000000000000000815260206004820152601c60248201527f42616420526571756573743a20276e616d652720697320656d70747900000000604482015290519081900360640190fd5b6000429050600060028054905083836040516020018084815260200183805190602001908083835b602083106104cc5780518252601f1990920191602091820191016104ad565b51815160209384036101000a600019018019909216911617905292019384525060408051808503815293820181528351938201939093206fffffffffffffffffffffffffffffffff1980821660009081526001808552959020805490911670010000000000000000000000000000000083041781558951919750879650945061055c93850192509088019061072e565b50600280548282018190556001808201835560008381527f405787fa12a823e0f2b7631cc41b3ba8828b3321ca811111fa75cd3aa3bb5ace9383049390930180546fffffffffffffffffffffffffffffffff929093166010026101000a9182021990921670010000000000000000000000000000000086049190910217905560408051602080825288518183015288516fffffffffffffffffffffffffffffffff1987169433947fca538c4b8d21e6ce75d513ef46eba5db55e1185324f96231315f207e77dddcf2948c949093849392840192918601918190849084905b8381101561065257818101518382015260200161063a565b50505050905090810190601f16801561067f5780820380516001836020036101000a031916815260200191505b509250505060405180910390a35050505050565b60025460009015156106a757506000610322565b6fffffffffffffffffffffffffffffffff1982166000818152600160205260409020600290810154815481106106d957fe5b90600052602060002090600291828204019190066010029054906101000a9004700100000000000000000000000000000000026fffffffffffffffffffffffffffffffff1916149050919050565b6002545b90565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061076f57805160ff191683800117855561079c565b8280016001018555821561079c579182015b8281111561079c578251825591602001919060010190610781565b506107a89291506107ac565b5090565b61072b91905b808211156107a857600081556001016107b256fe42616420526571756573743a2027696e646578272073686f756c64206265203e3d2030a165627a7a72305820d71715e6e44a54fa434dfd01d32a5d8f0a400a87b6992bfdfbebb0fc13f4a7f60029";

        public static async Task<string> DeployContractAsync(Web3 web3, string addressFrom,  CancellationTokenSource token = null, HexBigInteger gas = null)
        {
            if (gas == null)
            {
                BigInteger estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(ABI, ByteCode, addressFrom).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            var transactionReceipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(ABI, ByteCode, addressFrom, gas, token).ConfigureAwait(false);
            return transactionReceipt.ContractAddress;
        }

        private readonly Web3 _web3;
        private readonly Contract _contract;

        public SupplyChainLogContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<ISupplyChainLogContractService, Task<string>> func, int timeoutInSeconds)
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

        private Function GetFunctionGetOrderIdAtIndex()
        {
            return _contract.GetFunction("getOrderIdAtIndex");
        }

        private Function GetFunctionGetOrderById()
        {
            return _contract.GetFunction("getOrderById");
        }

        private Function GetFunctionAddOrder()
        {
            return _contract.GetFunction("addOrder");
        }

        private Function GetFunctionIsExistingOrder()
        {
            return _contract.GetFunction("isExistingOrder");
        }

        private Function GetFunctionGetOrderCount()
        {
            return _contract.GetFunction("getOrderCount");
        }


        public Event<AddOrderEvent> GetAddOrderEvent()
        {
            return _contract.GetEvent<AddOrderEvent>("AddOrderEvent");
        }


        public Task<byte[]> GetOrderIdAtIndexCallAsync(BigInteger index)
        {
            var function = GetFunctionGetOrderIdAtIndex();
            return function.CallAsync<byte[]>(index);
        }

        public async Task<byte[]> GetOrderIdAtIndexCallAsync(string addressFrom, BigInteger index, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetOrderIdAtIndex();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, index).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<byte[]>(addressFrom, gas, valueAmount, index).ConfigureAwait(false);
        }

        public Task<bool> IsExistingOrderCallAsync(byte[] id)
        {
            var function = GetFunctionIsExistingOrder();
            return function.CallAsync<bool>(id);
        }

        public async Task<bool> IsExistingOrderCallAsync(string addressFrom, byte[] id, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionIsExistingOrder();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, id).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<bool>(addressFrom, gas, valueAmount, id).ConfigureAwait(false);
        }

        public Task<BigInteger> GetOrderCountCallAsync()
        {
            var function = GetFunctionGetOrderCount();
            return function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> GetOrderCountCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetOrderCount();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }


        public async Task<string> AddOrderAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddOrder();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
        }


        public Task<GetOrderById> GetOrderByIdCallAsync(byte[] orderId)
        {
            var function = GetFunctionGetOrderById();
            return function.CallDeserializingToObjectAsync<GetOrderById>(orderId);
        }

        public async Task<GetOrderById> GetOrderByIdCallAsync(string addressFrom, byte[] orderId,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetOrderById();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, orderId).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallDeserializingToObjectAsync<GetOrderById>(addressFrom, gas, valueAmount, orderId).ConfigureAwait(false);
        }



    }

    [FunctionOutput]
    public class GetOrderById
    {
        [Parameter("bytes16", "id", 1)]
        public byte[] Id { get; set; }

        [Parameter("string", "name", 2)]
        public string Name { get; set; }

    }


    [Event("AddOrderEvent")]
    public class AddOrderEvent : IEventDTO
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; }

        [Parameter("bytes16", "id", 2, true)]
        public byte[] Id { get; set; }

        [Parameter("string", "name", 3, false)]
        public string Name { get; set; }

    }


}
