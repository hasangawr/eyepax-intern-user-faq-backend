using FaqService.Dal;
using FaqService.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service
{
    public class UserService : IUserService
    {
        private readonly IFaqRepo _faqRepo;

        public UserService(IFaqRepo faqRepo)
        {
            _faqRepo = faqRepo;
        }

        public bool UserExists(Guid userId)
        {
            return _faqRepo.UserExists(userId);
        }
    }
}
