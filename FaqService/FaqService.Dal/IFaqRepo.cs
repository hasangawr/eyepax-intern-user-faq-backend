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
        void CreateQuestion(Question question);
        bool UserExists(int userId);
        //bool ExternalPlatformExists(int externalPlatformId);

        // Answers
        //Command GetCommand(int platformId, int commandId);
        //void CreateCommand(int platformId, Command command);
    }
}
