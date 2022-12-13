﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var exchangeName = "notifier";
var queueName = "sms-queue";
var factory = new ConnectionFactory { HostName = "localhost" };
var totalHold = 0;

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(
        exchange: exchangeName, 
        type: ExchangeType.Fanout);

    channel.QueueBind(
        queue: queueName,
        exchange: exchangeName,
        routingKey: string.Empty);

    Console.WriteLine("Waiting for payments...");

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (sender, e) => {
        var body = e.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());

        var payment = GetPayment(message);
        totalHold += payment;

        Console.WriteLine($"Received payment: {payment}");
        Console.WriteLine($"Total hold this session: {totalHold}");
    };

    channel.BasicConsume(
        queue: queueName,
        autoAck: true,
        consumer: consumer);

    Console.WriteLine($"Subscribed to the queue '{queueName}'");
    Console.WriteLine($"Listening...");

    Console.ReadLine();
}

int GetPayment(string message)
{
    var messageWords = message.Split(' ');
    return int.Parse(messageWords[^1]);
}
