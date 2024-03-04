using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FaqService.Dal
{
    public class FaqRepo : IFaqRepo
    {
        private readonly AppDbContext _dbContext;

        public FaqRepo(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public bool AnswerExists(int questionId, int answerId)
        {
            return _dbContext.Answers.Any(a => a.QuestionId == questionId && a.Id == answerId);
        }

        public void CreateAnswer(Answer answer)
        {
            _dbContext.Answers.Add(answer);
            _dbContext.SaveChanges();
        }

        public void CreateQuestion(Question question)
        {
            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();
        }

        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteAnswer(int answerId)
        {
            var answer = _dbContext.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer != null) 
            {
                _dbContext.Answers.Remove(answer);
            }
            _dbContext.SaveChanges();
        }

        public void DeleteQuestion(Question question)
        {
            _dbContext.Questions.Remove(question);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            var questions = _dbContext.Questions.ToList();
            Console.WriteLine("---> Question list");
            Console.WriteLine(questions);
            return questions;
        }

        public int GetAnswerDislikeCount(int id)
        {
            return _dbContext.Votes.Count(v => v.AnswerId == id && v.VoteType == "Dislike");
        }

        public int GetAnswerLikeCount(int id)
        {
            return _dbContext.Votes.Count(v => v.AnswerId == id && v.VoteType == "Like"); ;
        }

        public Guid GetAnswerOwnerId(int answerId)
        {
            return _dbContext.Answers
                            .Where(a => a.Id == answerId)
                            .FirstOrDefault()
                            .UserId;
        }

        public IEnumerable<Question> GetFrequentlyAskedQuestions()
        {
            throw new NotImplementedException();
        }

        public Question GetQuestion(int id)
        {
            return _dbContext.Questions
                            .Where(q => q.Id == id)
                            .FirstOrDefault();
        }

        public IEnumerable<Answer> GetQuestionAnswers(int questionId)
        {
            return _dbContext.Answers.Where(a => a.QuestionId == questionId);
        }

        public Guid GetQuestionOwnerId(int questionId)
        {
            return _dbContext.Questions
                            .Where(q => q.Id == questionId)
                            .FirstOrDefault()
                            .UserId;
        }

        public string GetUserChoice(int answerId, Guid userId)
        {
            var vote = _dbContext.Votes
                            .Where(v => v.AnswerId == answerId && v.UserId == userId)
                            .FirstOrDefault();

            if(vote != null) 
            {
                return vote.VoteType;
            }

            return "Default";
        }

        public bool QuestionExists(int questionId)
        {
            return _dbContext.Questions.Any(q => q.Id == questionId);
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public Answer UpdateAnswer(int questionId, int answerId, Answer answer)
        {
            var answerFromDb = _dbContext.Answers
                            .Where(a => a.QuestionId == questionId && a.Id == answerId)
                            .FirstOrDefault();

            answerFromDb.Description = answer.Description;

            _dbContext.SaveChanges();

            return _dbContext.Answers
                            .Where(a => a.QuestionId == questionId && a.Id == answerId)
                            .FirstOrDefault();
        }

        public Question UpdateQuestion(int questionId, Question question)
        {
            var questionFromDb = _dbContext.Questions
                            .Where(q => q.Id == questionId)
                            .FirstOrDefault();

            questionFromDb.Title = question.Title;
            questionFromDb.Description = question.Description;

            _dbContext.SaveChanges();

            return _dbContext.Questions
                            .Where(q => q.Id == questionId)
                            .FirstOrDefault();
        }


        public void UpdateVotes(Vote vote)
        {
            var voteInDb = _dbContext.Votes.Where(v => v.AnswerId == vote.AnswerId && v.UserId == vote.UserId)
                .FirstOrDefault();

            if (voteInDb != null) 
            {
                voteInDb.VoteType = vote.VoteType;
                _dbContext.SaveChanges();
                return;
            }

            _dbContext.Votes.Add(vote);
            _dbContext.SaveChanges();
        }

        public bool UserExists(Guid userId)
        {
            return _dbContext.Users.Any(u => u.Id == userId);
        }
    }
}
