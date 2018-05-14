using Microsoft.Azure.ServiceBus;
using System;
using System.Threading.Tasks;

namespace servicebuys_createsubwithfilter
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://cloudsolution.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=B0x250UzsG+mNXXNR+b0f/DxtaonQwJXHUyvbgD8zl4=";
        const string TopicName = "promcarmodel";
        const string SubscriptionName = "3series";
        static ISubscriptionClient subscriptionClient;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            
            subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
            var rules = new RuleDescription()
            {
                Filter = new SqlFilter("series = '3'"),
                Action = new SqlRuleAction("set label = '3 series'"),
                Name = "series"
            };
            await subscriptionClient.AddRuleAsync(rules);
        }
    }
}
