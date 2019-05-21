using System;
using System.Threading.Tasks;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts.Managed;

// Created with 'Nethereum-CodeGenerator' https://github.com/StefH/Nethereum-CodeGenerator by stef.heyenrath@mstack.nl (mStack B.V.)
namespace DefaultNamespace
{
    static class Program
    {
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount ContractOwner = new ManagedAccount("0x5f8206Cb73897BF21ECA2305b17DDc09FCC06eDA", "test");

        private static string _contractAddress;

        public static void Main(string[] args)
        {
            TestService().Wait(15 * 60 * 1000);
        }

        private static async Task TestService()
        {
            var web3 = new Web3Geth(ContractOwner, Endpoint);

            Console.WriteLine("Deploying Contract...");
            // _contractAddress = await SimpleStorageContractService.DeployContractAsync(web3, ContractOwner.Address, /* optional constructor parameters */ null, null);
            Console.WriteLine($"Deploying Contract done. Contract Address = {_contractAddress}");

            await TestOther();
        }

        private static async Task TestOther()
        {
            Console.WriteLine("TestOther");
            var web3 = new Web3Geth(ContractOwner, Endpoint);

            ISimpleStorageContractService service = new SimpleStorageContractService(web3, _contractAddress);

            // More code here...
        }
    }
}

