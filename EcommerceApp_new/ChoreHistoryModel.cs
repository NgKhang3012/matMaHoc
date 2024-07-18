using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Person;

namespace MongoDataAccess.Models;

public class ChoreHistoryModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ChoreId { get; set; }
    public string ChoreText { get; set; }
    public DateTime DateCompleted { get; set; }
    public PersonModel WhoCompleted { get; set; }

    public ChoreHistoryModel()
    {

    }

    public ChoreHistoryModel(ChoreModel chore)
    {
        ChoreId = chore.Id;
        DateCompleted = chore.LastCompleted ?? DateTime.Now;
        WhoCompleted = chore.AssignedTo;
        ChoreText = chore.ChoreText;
    }
}
