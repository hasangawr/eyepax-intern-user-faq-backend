using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Models
{
    public class Vote
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string VoteType { get; set; } = "default";

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Answer? Answer { get; set; }

    }
}