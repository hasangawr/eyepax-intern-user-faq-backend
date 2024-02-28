using FaqService.Domain.Dtos;
using FaqService.Domain.Models;
using FaqService.Service.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;

        public FaqController(IQuestionService questionService, IUserService userService) 
        {
            _questionService = questionService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<int>> GetFrequentlyAskedQuestions()
        {
            // TODO
            return Ok(new List<int>());
        }

        [HttpPost("questions")]
        public ActionResult CreateQuestion(int userId, QuestionCreateDto questionCreateDto)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound("User not found");
            }

            try
            {
                _questionService.CreateQuestion(userId, questionCreateDto);

            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            return Ok("Question successfully created");
        }

    }
}
