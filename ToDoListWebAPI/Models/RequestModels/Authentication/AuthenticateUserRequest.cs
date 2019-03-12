namespace ToDoListWebAPI.Models.RequestModels.Authentication
{
  public class AuthenticateUserRequest
  {
    public string UserId { get; set; }
    public string Password { get; set; }
  }
}
