using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public interface IGetToDoObjectService
  {
    Task<ToDoEntity> GetToDoObject(GetToDoObjectRequest request);
    Task<List<ToDoEntity>> GetAllToDoObjectsForUser(string userId);
  }
}