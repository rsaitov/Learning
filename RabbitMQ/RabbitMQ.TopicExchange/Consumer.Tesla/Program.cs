﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var routingKey = "Tesla.#";

var factory = new ConnectionFactory { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

    var queueName = channel.QueueDeclare().QueueName;

    channel.QueueBind(
        queue: queueName,
        exchange: "topic_logs",
        routingKey: routingKey);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (sender, e) =>
    {
        var body = e.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        Console.WriteLine($"Received message: {message}");
    };

    channel.BasicConsume(
        queue: queueName,
        autoAck: true,
        consumer: consumer);

    Console.WriteLine($"Subscribed to the queue '{queueName}'");
    Console.WriteLine($"Listening to [{routingKey}]");

    Console.ReadLine();
}