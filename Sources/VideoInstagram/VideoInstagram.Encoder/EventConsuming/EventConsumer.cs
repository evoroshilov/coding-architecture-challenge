using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.Encoder.EventConsuming
{
    internal class EventConsumer
    {
        private readonly ILogger _logger;

        public EventConsumer(ILogger logger)
        {
            _logger = logger;
        }

        private void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 10,
                AutoComplete = true
            };

            _subscriptionClient.RegisterMessageHandler(HandleMessageAsync, messageHandlerOptions);
        }

        private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            try
            {

                var eventType = (string)message.UserProperties["EventType"];
                var messageBody = Encoding.UTF8.GetString(message.Body);
                var eventNotificationMessage = JsonConvert.DeserializeObject<VideoUploadMessage>(messageBody);

                //Do Encoding and save it in blob storage
                
            }
            catch (Exception e)
            {
               
            }
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            return Task.CompletedTask;
        }
    }
}
