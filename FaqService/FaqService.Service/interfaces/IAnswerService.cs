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
        void AddQuestionAnswers(int userId, int questionId, AnswerCreateDto answerCreateDto);
        bool AnswerExists(int questionId, int answerId);
        void DeleteAnswer(int answerId);

        //void CreateQuestion(int userId, QuestionCreateDto questionCreateDto);
        //Question DeleteQuestion(int questionId);
        //IEnumerable<QuestionReadDto> GetAllQuestions();
        //QuestionReadDto GetQuestion(int questionId);

        IEnumerable<AnswerReadDto> GetQuestionAnswers(int questionId);
        AnswerReadDto UpdateAnswer(int questionId, int answerId, AnswerCreateDto answerCreateDto);
    }
}
