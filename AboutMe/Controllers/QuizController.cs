using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly MyInfoContext _dbContext;

        public QuizController(MyInfoContext myInfoContext)
        {
            _dbContext = myInfoContext;
        }

        [HttpPost("addQuiz")]
        public IActionResult AddQuizs([FromBody]List<Quiz> quiz)
        {
            _dbContext.Questions.AddRange(quiz);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}