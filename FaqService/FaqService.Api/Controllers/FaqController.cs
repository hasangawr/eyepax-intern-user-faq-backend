using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using FaqService.Service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace FaqService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FaqController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;
        private readonly IAnswerService _answerService;
        private readonly IVotesService _votesService;

        public FaqController(
            IQuestionService questionService,
            IUserService userService,
            IAnswerService answerService,
            IVotesService votesService)
        {
            _questionService = questionService;
            _userService = userService;
            _answerService = answerService;
            _votesService = votesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<int>> GetFrequentlyAskedQuestions()
        {
            // TODO
            return Ok(new List<int>());
        }


        [HttpGet("questions")]
        public ActionResult<IEnumerable<QuestionReadDto>> GetAllQuestions()
        {
            return Ok(_questionService.GetAllQuestions());
        }


        [HttpPost("questions")]
        public ActionResult CreateQuestion(QuestionCreateDto questionCreateDto)
        {
            Guid userId = GetCurrentUserId();

            if (!_userService.UserExists(userId))
            {
                return NotFound("User not found");
            }

            try
            {
                _questionService.CreateQuestion(userId, questionCreateDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Question successfully created");
        }


        [HttpGet("questions/{questionId}")]
        public ActionResult<QuestionReadDto> GetQuestionById(int questionId)
        {
            if (!_questionService.QuestionExists(questionId))
            {
                return NotFound("Question not found");
            }

            return Ok(_questionService.GetQuestion(questionId));
        }


        [HttpPut("questions/{questionId}")]
        public ActionResult<QuestionReadDto> UpdateQuestion([FromRoute] int questionId,
            [FromBody] QuestionCreateDto questionCreateDto)
        {
            Guid userId = GetCurrentUserId();

            if (!_questionService.QuestionExists(questionId))
            {
                return NotFound("Question not found");
            }

            if (!(_questionService.GetQuestionOwnerId(questionId) == userId)) 
            {
                return Forbid();
            }

            QuestionReadDto question;

            try
            {
                question = _questionService.UpdateQuestion(questionId, questionCreateDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(question);
        }


        [HttpDelete("questions/{questionId}")]
        public ActionResult DeleteQuestion(int questionId)
        {
            Guid userId = GetCurrentUserId();

            if (!(_questionService.GetQuestionOwnerId(questionId) == userId))
            {
                return Forbid();
            }

            var question = _questionService.DeleteQuestion(questionId);

            if (question == null)
            {
                return NotFound("Question Not Found");
            }

            return Ok("Question successfully deleted");
        }


        [HttpGet("questions/{questionId}/answers")]
        public ActionResult<IEnumerable<AnswerReadDto>> GetQuestionAnswers(int questionId)
        {
            Guid userId = GetCurrentUserId();

            return Ok(_answerService.GetQuestionAnswers(userId, questionId));
        }


        [HttpPost("questions/{questionId}/answers")]
        public ActionResult AddQuestionAnswers
            (int questionId, AnswerCreateDto answerCreateDto)
        {
            Guid userId = GetCurrentUserId();

            if (!_userService.UserExists(userId))
            {
                return NotFound("User not found");
            }

            if (!_questionService.QuestionExists(questionId))
            {
                return NotFound("Question not found");
            }

            try
            {
                _answerService.AddQuestionAnswers(userId, questionId, answerCreateDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Answer successfully added");
        }


        [HttpPut("questions/{questionId}/answers/{answerId}")]
        public ActionResult<AnswerReadDto> UpdateAnswer
            (int answerId, int questionId, AnswerCreateDto answerCreateDto)
        {
            Guid userId = GetCurrentUserId();

            if (!_questionService.QuestionExists(questionId))
            {
                return NotFound("Question not found");
            }

            if (!_answerService.AnswerExists(questionId, answerId))
            {
                return NotFound("Answer not found");
            }

            if (!(_answerService.GetAnswerOwnerId(questionId,answerId) == userId))
            {
                return Forbid();
            }

            AnswerReadDto answerReadDto;

            try
            {
                answerReadDto = _answerService.UpdateAnswer(userId, questionId, answerId, answerCreateDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(answerReadDto);
        }


        [HttpDelete("questions/{questionId}/answers/{answerId}")]
        public ActionResult DeleteAnswer(int questionId, int answerId)
        {
            Guid userId = GetCurrentUserId();

            if (!_questionService.QuestionExists(questionId))
            {
                return NotFound("Question not found");
            }

            if (!_answerService.AnswerExists(questionId, answerId))
            {
                return NotFound("Answer not found");
            }

            if (!(_answerService.GetAnswerOwnerId(questionId, answerId) == userId))
            {
                return Forbid();
            }

            try
            {
                _answerService.DeleteAnswer(answerId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok("Answer successfully deleted");
        }


        // VOTES
        [HttpPost("votes/{answerId}")]
        public ActionResult UpdateVotes(int answerId, VoteCreateDto voteCreateDto)
        {
            Guid userId = GetCurrentUserId();

            if (!_userService.UserExists(userId))
            {
                return NotFound("User not found");
            }

            try
            {
                _votesService.UpdateVotes(userId, answerId, voteCreateDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Vote successfully updated");
        }



        //get userId from the token
        private Guid GetCurrentUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userIdClaim = identity.Claims.First(c => c.Type == "UserId").Value;

                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }

            }
            return Guid.Empty;

        }

    }

}
