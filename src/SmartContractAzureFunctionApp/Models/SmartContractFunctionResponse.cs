﻿using JetBrains.Annotations;

namespace SmartContractAzureFunctionApp.Models
{
    [PublicAPI]
    public class SmartContractFunctionResponse
    {
        public ulong GasEstimated { get; set; }

        public ulong? GasUsed { get; set; }

        public object Response { get; set; }

        public string TransactionHash { get; set; }
    }
}