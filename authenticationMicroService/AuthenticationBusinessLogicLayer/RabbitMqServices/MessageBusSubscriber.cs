using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using AuthenticationDataAccessLayer.AuthenticationRepo;
using AuthenticationDataAccessLayer.Entities;
using System.Text.Json;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace AuthenticationBusinessLogicLayer.RabbitMqServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider; // Inject service provider

        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriber(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider; // Inject service provider
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: _queueName,
                exchange: "trigger",
                routingKey: "");

            Console.WriteLine("---> Listening on Message Bus...");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("---> Connection Shutdown");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
                //Console.WriteLine(notificationMessage);

                // Resolve IAuthenticationRepo using service provider
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authenticationRepo = scope.ServiceProvider.GetRequiredService<IAuthenticationRepo>();
                    UserMessage newUserMessage = JsonSerializer.Deserialize<UserMessage>(notificationMessage);
                    if (newUserMessage.MessageType.Equals("Add") && newUserMessage.EventType.Equals("UserToAuthMessage"))
                    {
                        Console.WriteLine(newUserMessage.EventType);
                        await authenticationRepo.SaveUser(newUserMessage.Id, newUserMessage.UserName, newUserMessage.Password);
                    }
                    else if (newUserMessage.MessageType.Equals("Delete") && newUserMessage.EventType.Equals("UserToAuthMessage"))
                    {
                        await authenticationRepo.DeleteUserAsync(newUserMessage.Id);
                    }
                    else if (newUserMessage.MessageType.Equals("Update") && newUserMessage.EventType.Equals("UserToAuthMessage"))
                    {
                        await authenticationRepo.UpdateUserAsync(newUserMessage.Id, newUserMessage.UserName, newUserMessage.Password);
                    }
                    else
                    {
                        Console.WriteLine($"---> Auth Table update Error");
                    }
                    
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }
    }
}
