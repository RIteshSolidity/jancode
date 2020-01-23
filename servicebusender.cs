using System;
using Microsoft.Azure.ServiceBus;


namespace TopicmessageSender
{
    class Program
    {

        private static string connectionString = "Endpoint=sb://mclassjansb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=HPST2w0JHwJb+Xg/L76ipCuvH/NW3/5Hj2Nm7G5iM8c=";
        private static string topicName = "ordertopic";

        static void Main(string[] args)
        {
            TopicClient client = new TopicClient(connectionString, topicName);
            Message msg = new Message();
            msg.Body = System.Text.Encoding.ASCII.GetBytes("THis is a order message");

            client.SendAsync(msg).GetAwaiter().GetResult();
            Console.WriteLine("Message Send");
        }
    }
}
