using AutoMapper;
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
    public class AnswerService : IAnswerService
    {
        private readonly IFaqRepo _faqRepo;
        private readonly IMapper _mapper;

        public AnswerService(IFaqRepo faqRepo, IMapper mapper) 
        {
            _faqRepo = faqRepo;
            _mapper = mapper;
        }
        public IEnumerable<AnswerReadDto> GetQuestionAnswers(int questionId)
        {
            var answers = _faqRepo.GetQuestionAnswers(questionId);

            return _mapper.Map<IEnumerable<AnswerReadDto>>(answers);
        }
    }
}
