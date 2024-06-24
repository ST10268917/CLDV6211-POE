using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunctionsPOE
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<List<string>> RunOrchestrator(
        [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Assume 'orderDetails' is an object you pass into the orchestrator which contains necessary order info.
            var orderDetails = new { Amount = 100.00, Message = "Order Details" };

            // Ensure that the correct type is passed to each function.
            outputs.Add(await context.CallActivityAsync<string>("UpdateInventory", "Inventory Info"));
            outputs.Add(await context.CallActivityAsync<string>("UpdateOrderHistory", "History Info"));
            outputs.Add(await context.CallActivityAsync<string>("ProcessPayment", orderDetails.Amount));
            outputs.Add(await context.CallActivityAsync<string>("ConfirmOrder", orderDetails.Amount));

            return outputs;
        }



        [FunctionName("UpdateInventory")]
        public static string UpdateInventory([ActivityTrigger] string input, ILogger log)
        {
            // Implementation for update inventory
            log.LogInformation("Updating inventory");
            return "Inventory updated";
        }

        [FunctionName("UpdateOrderHistory")]
        public static string UpdateOrderHistory([ActivityTrigger] string input, ILogger log)
        {
            // Implementation for updating order history
            log.LogInformation("Updating order history");
            return "Order history updated";
        }

        [FunctionName("ProcessPayment")]
        public static string ProcessPayment([ActivityTrigger] double amount, ILogger log)
        {
            // Implementation for processing payment 
            log.LogInformation($"Processing payment: ${amount}.");
            return $"Payment processed: ${amount}.";
        }

        [FunctionName("ConfirmOrder")]
        public static string ConfirmOrder([ActivityTrigger] double amount, ILogger log)
        {
            log.LogInformation("Confirming order");
            return "Order confirmation sent";
        }


        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}