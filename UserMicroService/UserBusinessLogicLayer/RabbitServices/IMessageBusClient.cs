using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataAccessLayer.Entities;

namespace UserBusinessLogicLayer.RabbitServices
{
    public interface IMessageBusClient
    {
        void PublishNewUser(UserMessage platformPublishedDto);
    }
}
