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

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public bool UserExists(int userId)
        {
            return _dbContext.Users.Any(u => u.Id == userId);
        }
    }
}
