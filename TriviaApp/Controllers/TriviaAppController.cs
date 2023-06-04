using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TriviaApp.DataAccess;
using TriviaApp.Models;

namespace TriviaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TriviaAppController : ControllerBase
    {
        private readonly ITriviaAppRepository _triviaAppRepository;
        public TriviaAppController(ITriviaAppRepository triviaAppRepository)
        {
            _triviaAppRepository = triviaAppRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<TriviaQuestions>> GetQuestions()
        {
            return await _triviaAppRepository.GetQuestionsAsync();
        }
        [HttpGet("random")]
        public async Task<ActionResult<TriviaQuestions>> GetQuestion()
        {
            return await _triviaAppRepository.GetRandomQuestionAsync();
        }
        [HttpPut("{question}")]
        public async Task<ActionResult<TriviaQuestions>> PutQuestion([FromBody] TriviaQuestions item, string question)
        {
            if (question != item.Question) return BadRequest();
            await _triviaAppRepository.UpdateQuestionAsync(item);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> PostQuestion(TriviaQuestions question)
        {
            await _triviaAppRepository.CreateQuestionAsync(question);
            return NoContent();
        }
        [HttpDelete("{question}")]
        public async Task<IActionResult> DeleteQuestion(string question)
        {
            var questionToDelete = await _triviaAppRepository.GetQuestionAsync(question);
            if (questionToDelete == null)
            {
                return NotFound();
            }
            await _triviaAppRepository.DeleteQuestionAsync(questionToDelete.Question!);
            return NoContent();
        }
    }
}