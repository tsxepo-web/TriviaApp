using TriviaApp.Models;

namespace TriviaApp.DataAccess;
public interface ITriviaAppRepository
{
    Task<IEnumerable<TriviaQuestions>> GetQuestionsAsync();
    Task<TriviaQuestions> GetRandomQuestionAsync();
    Task<TriviaQuestions> GetQuestionAsync(string question);
    Task CreateQuestionAsync(TriviaQuestions question);
    Task UpdateQuestionAsync(TriviaQuestions question);
    Task DeleteQuestionAsync(string question);
}