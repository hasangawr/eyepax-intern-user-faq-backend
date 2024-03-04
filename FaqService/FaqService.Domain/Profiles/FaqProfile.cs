using AutoMapper;
using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FaqService.Domain.Profiles
{
    public class FaqProfile : Profile
    {
        public FaqProfile() 
        {
            // source -> target
            CreateMap<QuestionCreateDto, Question>();
            CreateMap<Question, QuestionReadDto>();
            CreateMap<Answer, AnswerReadDto>();
            CreateMap<AnswerCreateDto, Answer>();
            CreateMap<VoteCreateDto, Vote>();
            CreateMap<Vote, VoteReadDto>();
            CreateMap<UserPublishedDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName));
        }
    }
}
