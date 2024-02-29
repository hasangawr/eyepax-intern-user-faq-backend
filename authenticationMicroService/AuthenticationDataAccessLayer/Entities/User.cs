

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationDataAccessLayer.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
