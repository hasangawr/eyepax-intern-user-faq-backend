using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Domain.Dtos
{
    public class GenericEventDto
    {
        public string MessageType {  get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
    }
}
