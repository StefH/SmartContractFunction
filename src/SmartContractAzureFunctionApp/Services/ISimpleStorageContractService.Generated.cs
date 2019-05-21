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
    public interface ISimpleStorageContractService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<ISimpleStorageContractService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);



        Task<string> GetStringCallAsync();
        Task<string> GetStringCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> AddNumbersCallAsync(BigInteger number1, BigInteger number2);
        Task<BigInteger> AddNumbersCallAsync(string addressFrom, BigInteger number1, BigInteger number2, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> GetNumberCallAsync();
        Task<BigInteger> GetNumberCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> SetNumberAsync(string addressFrom, BigInteger value, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetStringAsync(string addressFrom, string value, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<GetVersion> GetVersionCallAsync();
        Task<GetVersion> GetVersionCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

    }
}
