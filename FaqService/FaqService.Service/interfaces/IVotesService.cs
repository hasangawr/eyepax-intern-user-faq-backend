using FaqService.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service.interfaces
{
    public interface IVotesService
    {
        void UpdateVotes(Guid userId, int answerId, VoteCreateDto voteCreateDto);
    }
}
