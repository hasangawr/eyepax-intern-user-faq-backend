using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Models
{
    public class Answer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User? User { get; set; }
        public Question? Question { get; set; }

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
