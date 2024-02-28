using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        //[StringLength(50, MinimumLength = 2)]
        public string? Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? UserName { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
