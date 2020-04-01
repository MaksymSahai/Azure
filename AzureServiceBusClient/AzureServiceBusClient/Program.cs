using Microsoft.Azure.ServiceBus;
using System;
using System.Text;

namespace AzureServiceBusClient
{
    class Program
    {
        static QueueClient queueClient;

        static void Main(string[] args)
        {
            string sbConnectionString = "Endpoint=sb://mobilerechargesb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2l/MZ8o/5jihLGjv7XXNM6fvEWJV2rgN27BSLDM7F2Q=";
            string sbQueueName = "recharge";

            string messageBody = string.Empty;
            try
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Mobile Recharge");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Operators");
                Console.WriteLine("1. Vodafone");
                Console.WriteLine("2. Life");
                Console.WriteLine("3. Kyivstar");
                Console.WriteLine("-------------------------------------------------------");

                Console.WriteLine("Operator:");
                string mobileOperator = Console.ReadLine();
                Console.WriteLine("Amount:");
                string amount = Console.ReadLine();
                Console.WriteLine("Mobile:");
                string mobile = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------------");

                switch (mobileOperator)
                {
                    case "1":
                        mobileOperator = "Vodafone";
                        break;
                    case "2":
                        mobileOperator = "Life";
                        break;
                    case "3":
                        mobileOperator = "Kyivstar";
                        break;
                    default:
                        break;
                }

                messageBody = mobileOperator + "*" + mobile + "*" + amount;
                queueClient = new QueueClient(sbConnectionString, sbQueueName);

                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                Console.WriteLine($"Message Added in Queue: {messageBody}");
                DateTimeOffset scheduleTime = DateTime.UtcNow.AddMinutes(5);
                queueClient.ScheduleMessageAsync(message, scheduleTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                queueClient.CloseAsync();
            }
        }
    }
}
