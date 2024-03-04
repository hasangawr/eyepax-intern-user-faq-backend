using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataAccessLayer.Entities
{
    public class UserToFaqMessage
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }

        public string? EventType {  get; set; }
        public string? MessageType { get; set; }
    }
}
