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

namespace SmartContractAzureFunctionApp
{
    public sealed class SmartContractFunctions
    {
        private readonly ISmartContractService _service;
        private readonly ILogger<SmartContractFunctions> _logger;

        /// <summary>
        /// Custom JsonSerializerSettings to make sure that null values are not serialized.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public SmartContractFunctions(ILogger<SmartContractFunctions> logger, ISmartContractService service)
        {
            _logger = logger;
            _service = service;
        }

        [FunctionName("DeploySmartContract")]
        public async Task<IActionResult> RunDeployContractAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req)
        {
            _logger.LogInformation("RunDeployContract");

            string body = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SmartContractDeployRequest>(body);

            try
            {
                var result = await _service.DeployContractAsync(request);

                return new JsonResult(result, JsonSerializerSettings);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "RunDeployContract failed");
                return new JsonResult(new { exception.Message }, JsonSerializerSettings);
            }
        }

        [FunctionName("QuerySmartContractFunction")]
        public async Task<IActionResult> RunQueryFunctionAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req)
        {
            _logger.LogInformation("QuerySmartContractFunction");

            string body = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SmartContractFunctionRequest>(body);

            try
            {
                var result = await _service.QueryFunctionAsync(request);

                return new JsonResult(result, JsonSerializerSettings);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "QuerySmartContractFunction failed");
                return new JsonResult(new { exception.Message }, JsonSerializerSettings);
            }
        }

        [FunctionName("ExecuteSmartContractFunction")]
        public async Task<IActionResult> RunExecuteFunctionAsync([HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req)
        {
            _logger.LogInformation("ExecuteSmartContractFunction");

            string body = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SmartContractFunctionRequest>(body);

            try
            {
                var result = await _service.ExecuteFunctionAsync(request);

                return new JsonResult(result, JsonSerializerSettings);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "ExecuteSmartContractFunction failed");
                return new JsonResult(new { exception.Message }, JsonSerializerSettings);
            }
        }
    }
}