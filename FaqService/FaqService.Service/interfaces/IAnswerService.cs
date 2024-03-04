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
        void AddQuestionAnswers(Guid userId, int questionId, AnswerCreateDto answerCreateDto);
        bool AnswerExists(int questionId, int answerId);
        void DeleteAnswer(int answerId);

        //void CreateQuestion(int userId, QuestionCreateDto questionCreateDto);
        //Question DeleteQuestion(int questionId);
        //IEnumerable<QuestionReadDto> GetAllQuestions();
        //QuestionReadDto GetQuestion(int questionId);

        IEnumerable<AnswerReadDto> GetQuestionAnswers(Guid userId, int questionId);
        AnswerReadDto UpdateAnswer(Guid userId, int questionId, int answerId, AnswerCreateDto answerCreateDto);
        Guid GetAnswerOwnerId(int questionId, int answerId);
    }
}
