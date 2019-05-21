using System;

namespace AzureFunctions.Common
{
    public static class AzureFunctionUtils
    {
        public static bool IsAzureEnvironment()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));
        }
    }
}