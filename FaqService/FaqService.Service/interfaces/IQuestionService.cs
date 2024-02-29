using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service.interfaces
{
    public interface IQuestionService
    {
        void CreateQuestion(int userId, QuestionCreateDto questionCreateDto);
        IEnumerable<QuestionReadDto> GetAllQuestions();

        QuestionReadDto GetQuestion(int questionId);
    }
}
