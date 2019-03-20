using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;

namespace ToDoListWebAPI.Services
{
  public interface IDBInterface
  {
    //Task<T> CreateAsync<T>(T entity);
    Task<T> CreateAsync<T>(ToDoEntity entity) where T : class;

    Task<ToDoEntity> Read(string userId, string title);

    Task<List<ToDoEntity>> ReadAllByUserId(string userId);

    Task Update<T>(string userId, string existingTitle, string description, string priority, string newTitle = null)
      where T : class;

    Task Delete<T>(string connectionString, string table, string userId, string title)
      where T : class;
  }
}
