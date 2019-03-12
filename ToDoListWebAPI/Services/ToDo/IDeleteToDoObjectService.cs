using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public interface IDeleteToDoObjectService
  {
    Task<T> DeleteToDoObject<T>(ToDoEntity toDoEntity)
      where T : class;
  }
}
