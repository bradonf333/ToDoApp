namespace ToDoListWebAPI.Models.RequestModels
{
  public class DeleteToDoObjectRequest
  {
    public string UserId { get; set; }
    public string Title { get; set; }
  }
}