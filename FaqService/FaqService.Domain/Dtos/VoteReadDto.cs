using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Dtos
{
    public class VoteReadDto
    {
        public string VoteType { get; set; } = "default";

        public int AnswerId { get; set; }

        public Guid UserId { get; set; }
    }
}
