using RabbitMQ.Client;
using System.Text;

var counter = 0;

do
{
    int timeToSleep = random.Next(1000, 2000);
    Thread.Sleep(timeToSleep);

    var factory = new ConnectionFactory { HostName = "localhost" };

    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(
            exchange: "topic_logs", 
            type: ExchangeType.Topic);

        var routingKey = counter % 4 == 0
            ? "Tesla.red.fast.ecological"
            : counter % 5 == 0
                ? "Mercedes.exclusive.expensive.ecological"
                : GenerateRoutingKey();

        var message = $"Message type [{routingKey}] from publisher [N:{counter}]";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "topic_logs",
            routingKey: routingKey,
            basicProperties: null,
            body: body
         );

        Console.WriteLine($"Message type [{routingKey}] is sent into Topic Exchange N {counter++}");
    }
} while (true);

static string GenerateRoutingKey()
{
    return $"{cars[random.Next(0, cars.Count)]}.{colors[random.Next(0,colors.Count)]}";
}

public partial class Program
{
    static List<string> cars = new List<string> { "BMW", "Audi", "Tesla", "Mercedes" };
    static List<string> colors = new List<string> { "red", "white", "black" };
    static Random random = new Random();
}