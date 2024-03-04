using AutoMapper;
using FaqService.Dal;
using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using FaqService.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqService.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly IFaqRepo _faqRepo;
        private readonly IMapper _mapper;

        public AnswerService(IFaqRepo faqRepo, IMapper mapper) 
        {
            _faqRepo = faqRepo;
            _mapper = mapper;
        }

        public void AddQuestionAnswers(Guid userId, int questionId, AnswerCreateDto answerCreateDto)
        {
            if (answerCreateDto == null)
            {
                throw new ArgumentNullException(nameof(answerCreateDto));
            }

            var answer = _mapper.Map<Answer>(answerCreateDto);

            answer.UserId = userId;
            answer.QuestionId = questionId;
            _faqRepo.CreateAnswer(answer);
        }

        public bool AnswerExists(int questionId, int answerId)
        {
            return _faqRepo.AnswerExists(questionId, answerId);
        }

        public void DeleteAnswer(int answerId)
        {
            _faqRepo.DeleteAnswer(answerId);
        }

        public Guid GetAnswerOwnerId(int questionId, int answerId)
        {
            if (AnswerExists(questionId, answerId))
            {
                return _faqRepo.GetAnswerOwnerId(answerId);
            }
            return Guid.Empty;
        }

        public IEnumerable<AnswerReadDto> GetQuestionAnswers(Guid userId, int questionId)
        {
            var answers = _faqRepo.GetQuestionAnswers(questionId);

            var answerReadDtos = _mapper.Map<IEnumerable<AnswerReadDto>>(answers);

            foreach (var answer in answerReadDtos)
            {
                var likes = _faqRepo.GetAnswerLikeCount(answer.Id);
                var dislikes = _faqRepo.GetAnswerDislikeCount(answer.Id);
                var userChoice = _faqRepo.GetUserChoice(answer.Id, userId);

                answer.LikesCount = likes;
                answer.DislikesCount = dislikes;
                answer.UserChoice = userChoice;
            }

            return answerReadDtos;
        }

        public AnswerReadDto UpdateAnswer(Guid userId, int questionId, int answerId, AnswerCreateDto answerCreateDto)
        {
            var answer = _mapper.Map<Answer>(answerCreateDto);

            var result = _faqRepo.UpdateAnswer(questionId, answerId, answer);

            var resultMapped = _mapper.Map<AnswerReadDto>(result);

            var likes = _faqRepo.GetAnswerLikeCount(resultMapped.Id);
            var dislikes = _faqRepo.GetAnswerDislikeCount(resultMapped.Id);
            var userChoice = _faqRepo.GetUserChoice(resultMapped.Id, userId);

            resultMapped.LikesCount = likes;
            resultMapped.DislikesCount = dislikes;
            resultMapped.UserChoice = userChoice;

            return resultMapped;
        }
    }
}
