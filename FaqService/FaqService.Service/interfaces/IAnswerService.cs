using FaqService.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service.interfaces
{
    public interface IAnswerService
    {
        //void CreateQuestion(int userId, QuestionCreateDto questionCreateDto);
        //Question DeleteQuestion(int questionId);
        //IEnumerable<QuestionReadDto> GetAllQuestions();
        //QuestionReadDto GetQuestion(int questionId);

        IEnumerable<AnswerReadDto> GetQuestionAnswers(int questionId);
    }
}
