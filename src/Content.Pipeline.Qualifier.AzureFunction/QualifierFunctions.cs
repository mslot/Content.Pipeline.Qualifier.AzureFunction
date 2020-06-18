using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace Content.Pipeline.Qualifier.AzureFunction
{
    public class QualifierFunctions
    {
        [FunctionName("QualifierFunction")]
        [return: ServiceBus("cachebuster-topic", Connection = "Servicebus:ServicebusConnectionString")]
        public Task<string> Run(
        [ServiceBusTrigger("qualifier-topic","QualifierSubscription", Connection = "Servicebus:ServicebusConnectionString")]
        string message,
        Int32 deliveryCount,
        DateTime enqueuedTimeUtc,
        string messageId,
        ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");

            return Task.FromResult(message);
        }
    }
}
