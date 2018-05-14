using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace servicebus_publisher
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://cloudsolution.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=B0x250UzsG+mNXXNR+b0f/DxtaonQwJXHUyvbgD8zl4=";
        const string TopicName = "promcarmodel";
        static ITopicClient topicClient;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 7;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(numberOfMessages);
            Console.ReadKey();
            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the topic
                    PromoData model = new PromoData()
                    {
                        Title = $"Promotion Title {i}",
                        Content = $"Promotion Content {i}",
                        CreatedTime = DateTime.Now
                    };
                    string messageBody = JsonConvert.SerializeObject(model);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody))
                    {
                        CorrelationId = "high",
                        Label = "blue",
                        UserProperties =
                        {
                            { "series", "3" },
                            { "priority", "high" }
                        }
                    };
                    
                    // Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the topic
                    await topicClient.SendAsync(message);
                    Console.WriteLine($"Message sent successfully {messageBody}");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

        }
    }
}
