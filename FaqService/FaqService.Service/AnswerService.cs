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
    public class AnswerService : IAnswerService
    {
        private readonly IFaqRepo _faqRepo;
        private readonly IMapper _mapper;

        public AnswerService(IFaqRepo faqRepo, IMapper mapper) 
        {
            _faqRepo = faqRepo;
            _mapper = mapper;
        }

        public void AddQuestionAnswers(int userId, int questionId, AnswerCreateDto answerCreateDto)
        {
            if (answerCreateDto == null)
            {
                throw new ArgumentNullException(nameof(answerCreateDto));
            }

            var answer = _mapper.Map<Answer>(answerCreateDto);

            answer.UserId = userId;
            answer.QuestionId = questionId;
            _faqRepo.CreateAnswer(answer);
        }

        public bool AnswerExists(int questionId, int answerId)
        {
            return _faqRepo.AnswerExists(questionId, answerId);
        }

        public void DeleteAnswer(int answerId)
        {
            _faqRepo.DeleteAnswer(answerId);
        }

        public IEnumerable<AnswerReadDto> GetQuestionAnswers(int questionId)
        {
            var answers = _faqRepo.GetQuestionAnswers(questionId);

            return _mapper.Map<IEnumerable<AnswerReadDto>>(answers);
        }

        public AnswerReadDto UpdateAnswer(int questionId, int answerId, AnswerCreateDto answerCreateDto)
        {
            var answer = _mapper.Map<Answer>(answerCreateDto);

            var result = _faqRepo.UpdateAnswer(questionId, answerId, answer);

            return _mapper.Map<AnswerReadDto>(result);
        }
    }
}
