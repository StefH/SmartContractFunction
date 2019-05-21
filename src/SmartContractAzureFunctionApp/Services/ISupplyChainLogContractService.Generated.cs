using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

// Created with 'Nethereum-CodeGenerator' https://github.com/StefH/Nethereum-CodeGenerator by stef.heyenrath@mstack.nl (mStack B.V.)
// Based on abi-code-gen (https://github.com/Nethereum/abi-code-gen)
namespace DefaultNamespace
{
    public interface ISupplyChainLogContractService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<ISupplyChainLogContractService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);


        Event<AddOrderEvent> GetAddOrderEvent();

        Task<byte[]> GetOrderIdAtIndexCallAsync(BigInteger index);
        Task<byte[]> GetOrderIdAtIndexCallAsync(string addressFrom, BigInteger index, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> IsExistingOrderCallAsync(byte[] id);
        Task<bool> IsExistingOrderCallAsync(string addressFrom, byte[] id, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> GetOrderCountCallAsync();
        Task<BigInteger> GetOrderCountCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> AddOrderAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<GetOrderById> GetOrderByIdCallAsync(byte[] orderId);
        Task<GetOrderById> GetOrderByIdCallAsync(string addressFrom, byte[] orderId, HexBigInteger gas = null, HexBigInteger valueAmount = null);

    }
}
