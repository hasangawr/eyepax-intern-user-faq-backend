using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Models
{
    public class Question
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        //public ICollection<string>? Keywords { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }

}
