using Framework.Db.Mongo;

namespace ToDoListWebAPI.Models.EntityModels
{
  public class User : MongoEntry
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AccountRoles { get; set; }
    public string UserId { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
  }
}
