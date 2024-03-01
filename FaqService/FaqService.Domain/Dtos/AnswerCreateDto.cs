using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Dtos
{
    public class AnswerCreateDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
