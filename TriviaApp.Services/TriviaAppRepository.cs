using MongoDB.Driver;
using TriviaApp.DataAccess;
using TriviaApp.Models;

namespace TriviaApp.Services;

public class TriviaAppRepository : ITriviaAppRepository
{
    private readonly IMongoCollection<TriviaQuestions> _triviaCollection;
    public TriviaAppRepository(IMongoCollection<TriviaQuestions> triviaCollection)
    {
        _triviaCollection = triviaCollection;
    }
    public async Task CreateQuestionAsync(TriviaQuestions question)
    {
        await _triviaCollection.InsertOneAsync(question);
    }

    public async Task DeleteQuestionAsync(string question)
    {
        await _triviaCollection.DeleteOneAsync(x => x.Question == question);
    }

    public async Task<TriviaQuestions> GetRandomQuestionAsync()
    {
        var random = new Random();
        var totalCount = await _triviaCollection.CountDocumentsAsync(FilterDefinition<TriviaQuestions>.Empty);
        var randomIndex = random.Next(0, (int)totalCount);
        var randomQuestion = await _triviaCollection.Find(FilterDefinition<TriviaQuestions>.Empty)
            .Skip(randomIndex)
            .Limit(1)
            .FirstOrDefaultAsync();
        return randomQuestion;
    }
    public async Task<IEnumerable<TriviaQuestions>> GetQuestionsAsync()
    {
        return await _triviaCollection.Find(_ => true).ToListAsync();
    }

    public async Task<TriviaQuestions> GetQuestionAsync(string question)
    {
        return await _triviaCollection.Find(x => x.Question == question).FirstOrDefaultAsync();
    }

    public async Task UpdateQuestionAsync(TriviaQuestions question)
    {
        await _triviaCollection.ReplaceOneAsync(x => x.Question == question.Question, question);
    }
}
