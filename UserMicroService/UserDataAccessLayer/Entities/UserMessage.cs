using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataAccessLayer.Entities
{
    public class UserMessage
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? EventType { get; set; }
        public string? MessageType { get; set; }
    }
}
