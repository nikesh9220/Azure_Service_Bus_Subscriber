using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Nikesh.Subscriber.Models;

namespace Nikesh.Subscriber.Services
{
    public class SubscriberService
    {
        private const string ServiceBusPrimaryConnectionString = "[Insert Your Connection String]";
        private const string TopicName = "sender-publisher";
        private const string SubscriptionName = "recevier-subscriber";
        private static ISubscriptionClient _subscriptionClient;
        private static IDataService _dataService;
        public  SubscriberService(IDataService dataService)
        {
            _dataService = dataService;
        }
        public  async Task Run()
        {
            _subscriptionClient = new SubscriptionClient(
                ServiceBusPrimaryConnectionString,
                TopicName,
                SubscriptionName);

            RegisterAndReceiveMessages();

            Console.ReadKey();

            await _subscriptionClient.CloseAsync();
        }

        private static void RegisterAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessReceivedMessages, messageHandlerOptions);
        }

        private static async Task ProcessReceivedMessages(Message message, CancellationToken token)
        {
            InformationViewModel model = FromMessage(message);

            Console.Clear();
            Console.WriteLine($"{model.FirstName} - {model.LastName}");
            _dataService.InsertData(model);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static InformationViewModel FromMessage(Message message)
        {
            var model = JsonConvert.DeserializeObject<InformationViewModel>(
                Encoding.UTF8.GetString(message.Body));

            return model;
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");

            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }
    }
}