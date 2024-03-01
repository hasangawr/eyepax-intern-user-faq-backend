using FaqService.Dal;
using FaqService.Domain.Dtos;
using FaqService.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service
{
    public class VotesService : IVotesService
    {
        public VotesService(IFaqRepo faqRepo) 
        {
            _faqRepo = faqRepo;
        }
        public void UpdateVotes(int userId, int answerId, VoteCreateDto voteCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}
