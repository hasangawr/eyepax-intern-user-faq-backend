using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FaqService.Dal
{
    public interface IFaqRepo
    {
        bool SaveChanges();

        // Questions
        IEnumerable<Question> GetFrequentlyAskedQuestions();
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestion(int id);
        void CreateQuestion(Question question);
        bool UserExists(Guid userId);
        void CreateUser(User user);
        void CreateAnswer(Answer answer);
        void DeleteQuestion(Question question);
        IEnumerable<Answer> GetQuestionAnswers(int questionId);
        bool QuestionExists(int questionId);
        Question UpdateQuestion(int questionId, Question question);
        bool AnswerExists(int questionId, int answerId);
        Answer UpdateAnswer(int questionId, int answerId, Answer answer);
        void DeleteAnswer(int answerId);
        void UpdateVotes(Vote vote);
        int GetAnswerLikeCount(int id);
        int GetAnswerDislikeCount(int id);
        string GetUserChoice(int answerId, Guid userId);
        Guid GetQuestionOwnerId(int questionId);
        Guid GetAnswerOwnerId(int answerId);
    }
}
