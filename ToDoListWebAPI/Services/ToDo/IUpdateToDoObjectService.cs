using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public interface IUpdateToDoObjectService
  {
    Task<ToDoEntity> UpdateToDoObject(UpdateToDoObjectRequest request);
  }
}