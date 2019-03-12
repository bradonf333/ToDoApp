using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.ResponseModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public class DeleteToDoObjectService : IDeleteToDoObjectService
  {
    private readonly IDBInterface _dBInterface;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DeleteToDoObjectService> _logger;

    public DeleteToDoObjectService(
      IDBInterface dBInterface,
      IConfiguration configuration,
      ILogger<DeleteToDoObjectService> logger)
    {
      _dBInterface = dBInterface;
      _configuration = configuration;
      _logger = logger;
    }

    public async Task<T> DeleteToDoObject<T>(ToDoEntity toDoEntity)
      where T : class
    {
      var connectionString = _configuration.GetConnectionString("MongoConnection");
      var table = "todoobject";
      //var table = toDoEntity.TableName;
      var userId = toDoEntity.UserId;
      var title = toDoEntity.Title;
      var deletedObject = new DeleteToDoObjectResponse();

      try
      {
        await _dBInterface.Delete<ToDoEntity>(connectionString, table, userId, title);
        _logger.LogInformation("Successfully deleted ToDoObject");
        deletedObject.Title = title;
        return deletedObject as T;
      }
      catch (Exception ex)
      {
        _logger.LogError("Failed to delete the ToDoObject", ex);
      }

      return null;
    }
  }
}
