namespace ToDoListWebAPI.Models.RequestModels
{
  public class UpdateToDoObjectRequest
  {
    public string Description { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string NewTitle { get; set; }
  }
}
