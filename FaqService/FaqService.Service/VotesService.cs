using AutoMapper;
using FaqService.Dal;
using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
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
        private readonly IFaqRepo _faqRepo;
        private readonly IMapper _mapper;

        public VotesService(IFaqRepo faqRepo, IMapper mapper) 
        {
            _faqRepo = faqRepo;
            _mapper = mapper;
        }
        public void UpdateVotes(Guid userId, int answerId, VoteCreateDto voteCreateDto)
        {
            var vote = _mapper.Map<Vote>(voteCreateDto);
            vote.AnswerId = answerId;
            vote.UserId = userId;
            _faqRepo.UpdateVotes(vote);
        }
    }
}
