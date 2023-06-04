using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TriviaApp.Models;
public class TriviaQuestions
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Question { get; set; }
    public List<string>? Options { get; set; }
    public string? CorrectAnswer { get; set; }
    public List<string>? Distractors { get; set; }
}