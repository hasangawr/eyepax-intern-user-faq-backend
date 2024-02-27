using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
