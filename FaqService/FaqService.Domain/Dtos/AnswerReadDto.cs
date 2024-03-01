using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Dtos
{
    public class AnswerReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string UserChoice { get; set; } = "Default";

        public Guid UserId { get; set; }
    }
}
