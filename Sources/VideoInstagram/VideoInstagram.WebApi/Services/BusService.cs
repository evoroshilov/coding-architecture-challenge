using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.WebApi.Services
{
    public class BusService
    {
        private readonly ILogger<BusService> _logger;
        private readonly TopicClient _topicClient;

        public BusService(ILogger<BusService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));


            _topicClient = new TopicClient("con string", "topic name");
        }

        public Task TryUploadMessageAsync(VideoUploadMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            return CreateAndSendTopicMessageAsync(message);
        }

        private async Task CreateAndSendTopicMessageAsync(VideoUploadMessage message)
        {
            try
            {
                if (_topicClient == null)
                {
                    return;
                }

                var tracingId = Guid.NewGuid().ToString();

                var payload = JsonConvert.SerializeObject(message, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                var topicMessage = new Message(Encoding.UTF8.GetBytes(payload)) { TimeToLive = TimeSpan.FromHours(2) };

                topicMessage.UserProperties.Add("TracingId", tracingId);


                await _topicClient.SendAsync(topicMessage).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while uploading event to topic.");
            }
        }
    }
}
