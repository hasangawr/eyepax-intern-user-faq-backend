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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FaqService.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IFaqRepo _faqRepo;
        private readonly IMapper _mapper;

        public QuestionService(IFaqRepo faqRepo, IMapper mapper)
        {
            _faqRepo = faqRepo;
            _mapper = mapper;
        }

        public void CreateQuestion(Guid userId, QuestionCreateDto questionCreateDto)
        {
            if (questionCreateDto == null)
            {
                throw new ArgumentNullException(nameof(questionCreateDto));
            }

            var question = _mapper.Map<Question>(questionCreateDto);

            question.UserId = userId;
            _faqRepo.CreateQuestion(question);
        }

        public Question DeleteQuestion(int questionId)
        {
            var question = _faqRepo.GetQuestion(questionId);

            if (question != null) 
            {
                _faqRepo.DeleteQuestion(question);
            }

            return question;
        }

        public IEnumerable<QuestionReadDto> GetAllQuestions() 
        {
            var questions = _faqRepo.GetAllQuestions();

            return _mapper.Map<IEnumerable<QuestionReadDto>>(questions);
        }

        public QuestionReadDto GetQuestion(int questionId) 
        {
            var question = _faqRepo.GetQuestion(questionId);

            if (question == null)
            {
                throw new ArgumentNullException($"Question {questionId} is not found");
            }

            return _mapper.Map<QuestionReadDto>(question);
        }

        public bool QuestionExists(int questionId)
        {
            return _faqRepo.QuestionExists(questionId);
        }

        public QuestionReadDto UpdateQuestion(int questionId, QuestionCreateDto questionCreateDto)
        {
            var question = _mapper.Map<Question>(questionCreateDto);

            var result = _faqRepo.UpdateQuestion(questionId, question);

            return _mapper.Map<QuestionReadDto>(result);
        }

        public Guid GetQuestionOwnerId(int questionId)
        {
            if (QuestionExists(questionId))
            {
                return _faqRepo.GetQuestionOwnerId(questionId);
            }
            return Guid.Empty;
        }


    }
}
