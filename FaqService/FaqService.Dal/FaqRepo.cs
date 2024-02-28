using FaqService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Dal
{
    public class FaqRepo : IFaqRepo
    {
        private readonly AppDbContext _dbContext;

        public FaqRepo(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void CreateQuestion(Question question)
        {
            _dbContext.Questions.Add(question);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetFrequentlyAskedQuestions()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int userId)
        {
            return _dbContext.Users.Any(u => u.Id == userId);
        }
    }
}
