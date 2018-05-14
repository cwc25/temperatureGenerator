using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Configuration;

namespace temperature_generator
{
    class Program
    {
        private static EventHubClient eventHubClient;


        public static IConfigurationRoot Configuration { get; set; } 
        public static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            MainAsync(args).GetAwaiter().GetResult();
        }
        private static async Task MainAsync(string[] args)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder($"{Configuration["ehConnectionString"]}")
            {
                EntityPath = $"{Configuration["ehEntityPath"]}"
            };
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            StreamReader reader = new StreamReader("shanghai temperature.csv");

            int count = 0;

            while(!reader.EndOfStream)
            {
                string tempData = reader.ReadLine();
                await SendMessageToEventHub(tempData);
                await Task.Delay(1000);
                count++;
            }
            Console.WriteLine(count);
            Console.ReadLine();
        }

        private static async Task SendMessageToEventHub(string message)
        {
            try
            {
                Console.WriteLine($"Sending Message: {message}");
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                Console.WriteLine($"Sending Completed for Message: {message}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
