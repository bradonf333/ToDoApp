using Framework.Db.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoListWebAPI.Models.EntityModels
{
  public class ToDoEntity : MongoEntry
  {
    [BsonElement("Description")]
    public string Description { get; set; }
    [BsonElement("Title")]
    public string Title { get; set; }
    [BsonElement("UserId")]
    public string UserId { get; set; }
    [BsonElement("Status")]
    public string Status { get; set; }
    [BsonElement("Priority")]
    public string Priority { get; set; }
    public string TableName { get; set; }

    public ToDoEntity(string userId, string title)
    {
      UserId = userId;
      Title = title;
      TableName = "todoobject";
    }

    public ToDoEntity()
    {
    }

    public override string ToString()
    {
      return "todo";
    }
  }
}
