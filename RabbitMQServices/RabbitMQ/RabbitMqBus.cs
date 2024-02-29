using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMqBus
    {
        public static IBusControl ConfigureBus(IBusRegistrationContext context, Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqBusConstants.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqBusConstants.UserName);
                    hst.Password(RabbitMqBusConstants.Password);
                });

                cfg.ConfigureEndpoints(context);

                registrationAction?.Invoke(cfg);
            });
        }

    }
}

