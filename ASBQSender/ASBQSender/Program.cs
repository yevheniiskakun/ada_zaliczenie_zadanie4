using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
namespace ASBQSender
{
    public class Program
    {
        /* The `<serviceBus-connection-string>` placeholder represents
           the connection string to the target Azure Service Bus namespace */
        private const string serviceBusConnectionString = "Endpoint=sb://asbqyevheniiskakun.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zcOD9exG43THgZK+ry7BrIs0HKH1bWs+b+ASbLXqYYk=";

        /* To create a string constant named "queueName" with a value
           of "messagequeue", matching the name of the Service Bus queue.*/
        private const string queueName = "messagesqueue";

        /* To create a Service Bus client that will own the connection to the target queue */
        static ServiceBusClient client = default!;

        /* To create a Service Bus sender that will be 
           used to publish messages to the target queue */
        static ServiceBusSender sender = default!;

        public static async Task Main(string[] args)
        {
            /* To initialize "client" of type "ServiceBusClient" that will 
               provide connectivity to the Service Bus namespace and "sender"
               that will be responsible for sending messages */
            client = new ServiceBusClient(serviceBusConnectionString);
            sender = client.CreateSender(queueName);

            /* Sending messages one by one */
            int i = 0;
            while (true)
            {
                await sender.SendMessageAsync(new ServiceBusMessage($"Message {i}"));
                Console.WriteLine("Sending: Message " + i.ToString());

                i++;
                Thread.Sleep(500);
            }
        }
    }
}