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

        public void CreateQuestion(int userId, QuestionCreateDto questionCreateDto)
        {
            if (questionCreateDto == null)
            {
                throw new ArgumentNullException(nameof(questionCreateDto));
            }

            var question = _mapper.Map<Question>(questionCreateDto);

            question.UserId = userId;
            _faqRepo.CreateQuestion(question);
        }
    }
}
