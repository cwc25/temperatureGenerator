using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Configuration;
namespace eventhub_receiver
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        //private static readonly string storageConnectionString = 
        //    $"DefaultEndpointsProtocol=https;AccountName={Configuration["StorageAccount"]};AccountKey={Configuration["Blobkey"]}";
        private static EventHubClient eventHubClient;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            eventHubClient = EventHubClient.CreateFromConnectionString(Configuration["EhConnectionString"]);
            var runtimeInformation = await eventHubClient.GetRuntimeInformationAsync();

            //foreach(var partitionId in runtimeInformation.PartitionIds)
            //{
            //    Console.WriteLine($"partitionid: {partitionId}");
            //}
            //Console.ReadLine();
            var partitionReceiver = eventHubClient.CreateReceiver(PartitionReceiver.DefaultConsumerGroupName, "0", "-1" );
            var ehEvents = await partitionReceiver.ReceiveAsync(5);

            foreach(var ehEvent in ehEvents)
            {
                var message = Encoding.UTF8.GetString(ehEvent.Body.Array);
                Console.WriteLine(message);
            }
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
