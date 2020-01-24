using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;

namespace MultiBinding
{

    public class Order
    {
        public string id { get; set; }
        public string description { get; set; }
        public string productid { get; set; }
        public int quantity { get; set; }

    }

    public class OrderInventory {
        public string id { get; set; }
        public string description { get; set; }
        public string productid { get; set; }
        public int availableQuantity { get; set; }
    }

    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [ServiceBusTrigger("orderqueue", Connection = "servicebusconnectionstring")]Order myQueueItem, 
            
            [CosmosDB("inventory","orderinventory", ConnectionStringSetting = "cosmosdbconnectionstring",
            Id = "{productid}", PartitionKey = "{productid}")] OrderInventory oinv,
            
            [CosmosDB("inventory", "orders", ConnectionStringSetting = "cosmosdbconnectionstring",
            Id = "{productid}", PartitionKey = "{productid}")] IAsyncCollector<Order> myorder,
            
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem.description}");
            log.LogInformation($"CosmosDB  message: {oinv.availableQuantity}");
            if (oinv.availableQuantity > myQueueItem.quantity) {
                myorder.AddAsync(myQueueItem);
            }
            
        }
    }
}
