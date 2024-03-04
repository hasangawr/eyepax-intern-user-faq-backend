using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserDataAccessLayer.Entities;

namespace UserBusinessLogicLayer.RabbitServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

                Console.WriteLine("---> Connected to the Message Bus");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"---> Could not connect to the message bus: {ex.Message}");
            }
        }

        public void PublishNewUserToAuthMs(UserMessage uMessage)
        {
            var message = JsonSerializer.Serialize(uMessage);

            if (_connection.IsOpen)
            {
                Console.WriteLine("---> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("---> RabbitMQ Connection is closed, not sending");
            }
        }
        public void PublishNewUserToFaqMs(UserToFaqMessage Message)
        {
            var message = JsonSerializer.Serialize(Message);

            if (_connection.IsOpen)
            {
                Console.WriteLine("---> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("---> RabbitMQ Connection is closed, not sending");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);

            Console.WriteLine($"---> We have sent {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("---> MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("---> RabbitMQ Connection Shutdown");
        }
    }
}
