using FaqService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Dal
{
    public class PrepDb
    {
        private readonly IFaqRepo _faqRepo;

        public PrepDb(IFaqRepo faqRepo) 
        {
            _faqRepo = faqRepo;
        }
        public void SeedData()
        {
            Console.WriteLine("Seeding new data...");

            //var users = new List<User> 
            //{
            //    new User
            //    {
            //        ExternalId = 1,
            //        Name = "Hasanga"
            //    },
            //    new User
            //    {
            //        ExternalId = 2,
            //        Name = "Damika"
            //    },
            //    new User
            //    {
            //        ExternalId = 3,
            //        Name = "Nusal"
            //    }
            //};

            //foreach (var user in users)
            //{
            //    _faqRepo.CreateUser(user);
            //    _faqRepo.SaveChanges();
            //}



        //    var questions = new List<Question>
        //    {
        //        new Question
        //        {
        //            Title = "Test Title 1",
        //            Description = "Test Description 1",
        //            UserId = 1
        //        },
        //        new Question
        //        {
        //            Title = "Test Title 2",
        //            Description = "Test Description 2",
        //            UserId = 1
        //        },
        //        new Question
        //        {
        //            Title = "Test Title 3",
        //            Description = "Test Description 3",
        //            UserId = 1
        //        },
        //        new Question
        //        {
        //            Title = "Test Title 4",
        //            Description = "Test Description 4",
        //            UserId = 2
        //        },
        //        new Question
        //        {
        //            Title = "Test Title 5",
        //            Description = "Test Description 5",
        //            UserId = 2
        //        },
        //        new Question
        //        {
        //            Title = "Test Title 6",
        //            Description = "Test Description 6",
        //            UserId = 3
        //        }
        //    };

        //    foreach (var question in questions)
        //    {
        //        _faqRepo.CreateQuestion(question);
        //        _faqRepo.SaveChanges();
        //    }



        //    var answers = new List<Answer>
        //    {
        //        new Answer
        //        {
        //            Description = "Test Answer 1",
        //            QuestionId = 1,
        //            UserId = 1,
        //        },
        //        new Answer
        //        {
        //            Description = "Test Answer 2",
        //            QuestionId = 1,
        //            UserId = 2,
        //        },
        //        new Answer
        //        {
        //            Description = "Test Answer 3",
        //            QuestionId = 1,
        //            UserId = 3,
        //        },
        //        new Answer
        //        {
        //            Description = "Test Answer 4",
        //            QuestionId = 2,
        //            UserId = 2,
        //        },
        //        new Answer
        //        {
        //            Description = "Test Answer 5",
        //            QuestionId = 2,
        //            UserId = 1,
        //        }

        //    };

        //    foreach (var answer in answers)
        //    {
        //        _faqRepo.CreateAnswer(answer);
        //        _faqRepo.SaveChanges();
        //    }


        //    var votes = new List<Vote>
        //    {
        //        new Vote
        //        {
        //            VoteType = "Like",
        //            AnswerId = 1,
        //            UserId = 1
        //        },
        //        new Vote
        //        {
        //            VoteType = "Dislike",
        //            AnswerId = 2,
        //            UserId = 1
        //        },
        //        new Vote
        //        {
        //            VoteType = "Like",
        //            AnswerId = 3,
        //            UserId = 2
        //        },
        //        new Vote
        //        {
        //            VoteType = "Like",
        //            AnswerId = 3,
        //            UserId = 1
        //        },
        //        new Vote
        //        {
        //            VoteType = "Like",
        //            AnswerId = 2,
        //            UserId = 2
        //        },
        //        new Vote
        //        {
        //            VoteType = "Dislike",
        //            AnswerId = 3,
        //            UserId = 2
        //        },
        //    };

        //    foreach (var vote in votes)
        //    {
        //        _faqRepo.UpdateVotes(vote);
        //        _faqRepo.SaveChanges();
        //    }
        }
    }
}
