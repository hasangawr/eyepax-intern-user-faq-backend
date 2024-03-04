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
        void PublishNewUserToAuthMs(UserMessage uMessage);
        void PublishNewUserToFaqMs(UserToFaqMessage UtoFaqMessage);
    }
}
