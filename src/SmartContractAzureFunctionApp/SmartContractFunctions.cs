using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartContractAzureFunctionApp.Models;
using SmartContractAzureFunctionApp.Services;
using System;
using System.Threading.Tasks;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace SmartContractAzureFunctionApp
{
    public static class SmartContractFunctions
    {
        /// <summary>
        /// Custom JsonSerializerSettings to make sure that null values are not serialized.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        [FunctionName("DeploySmartContract")]
        public static async Task<IActionResult> RunDeployContractAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req,
            [Inject] ISmartContractService service,
            ILogger logger)
        {
            logger.LogInformation("RunDeployContract");

            string body = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SmartContractDeployRequest>(body);

            try
            {
                var result = await service.DeployContractAsync(request);

                return new JsonResult(result, JsonSerializerSettings);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "RunDeployContract failed");
                return new JsonResult(new { exception.Message }, JsonSerializerSettings);
            }
        }

        [FunctionName("QuerySmartContractFunction")]
        public static async Task<IActionResult> RunQueryFunctionAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req,
            [Inject] ISmartContractService service,
            ILogger logger)
        {
            logger.LogInformation("QuerySmartContractFunction");

            string body = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SmartContractFunctionRequest>(body);

            try
            {
                var result = await service.QueryFunctionAsync(request);

                return new JsonResult(result, JsonSerializerSettings);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "QuerySmartContractFunction failed");
                return new JsonResult(new { exception.Message }, JsonSerializerSettings);
            }
        }

        [FunctionName("ExecuteSmartContractFunction")]
        public static async Task<IActionResult> RunExecuteFunctionAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req,
            [Inject] ISmartContractService service,
            ILogger logger)
        {
            logger.LogInformation("ExecuteSmartContractFunction");

            string body = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SmartContractFunctionRequest>(body);

            try
            {
                var result = await service.ExecuteFunctionAsync(request);

                return new JsonResult(result, JsonSerializerSettings);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "ExecuteSmartContractFunction failed");
                return new JsonResult(new { exception.Message }, JsonSerializerSettings);
            }
        }
    }
}