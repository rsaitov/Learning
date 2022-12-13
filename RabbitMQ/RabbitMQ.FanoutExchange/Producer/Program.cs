using RabbitMQ.Client;
using System.Text;

var random = new Random();
var exchangeName = "notifier";

do
{
    int timeToSleep = random.Next(1000, 3000);
    Thread.Sleep(timeToSleep);

    var factory = new ConnectionFactory { HostName = "localhost" };

    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(
            exchange: exchangeName, 
            type: ExchangeType.Fanout);

        var moneyCount = random.Next(1000, 10_000);
        var message = $"Payment received for the amount of {moneyCount}";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: exchangeName,
            routingKey: "",
            basicProperties: null,
            body: body
         );

        Console.WriteLine($"Payment received for amount of {moneyCount}.\nNotifying by '{exchangeName}' Exchange");
    }
} while (true);