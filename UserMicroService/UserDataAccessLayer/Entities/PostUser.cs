using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataAccessLayer.Entities
{
    public class PostUser
    {
        /*public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }*/
        [Required]
        [RegularExpression(@"^[a-zA-Z \s]+$", ErrorMessage = "First name characters only!")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[a-zA-Z \s]+$", ErrorMessage = "Last name characters only!")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]

        public string? Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        public string? Role { get; set; } =  string.Empty;
    }
}
