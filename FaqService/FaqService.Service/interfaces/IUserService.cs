using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service.interfaces
{
    public interface IUserService
    {
        public bool UserExists(Guid userId);
    }
}
