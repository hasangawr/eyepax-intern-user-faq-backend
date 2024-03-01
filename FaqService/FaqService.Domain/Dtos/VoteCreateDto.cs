using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Dtos
{
    public class VoteCreateDto
    {
        [Required]
        public string VoteType { get; set; } = "default";
    }
}
