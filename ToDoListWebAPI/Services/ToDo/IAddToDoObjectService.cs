using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public interface IAddToDoObjectService
  {
    Task<T> AddToDoObject<T>(ToDoEntity entity)
      where T : class;
  }
}