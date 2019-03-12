using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ToDoListWebAPI.Models;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public class UpdateToDoObjectService : IUpdateToDoObjectService
  {
    private readonly IDBInterface _dBInterface;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UpdateToDoObjectService> _logger;

    public UpdateToDoObjectService(
      IDBInterface dBInterface,
      IConfiguration configuration,
      ILogger<UpdateToDoObjectService> logger)
    {
      _dBInterface = dBInterface;
      _configuration = configuration;
      _logger = logger;
    }

    public async Task<ToDoEntity> UpdateToDoObject(UpdateToDoObjectRequest request)
    {
      var updatedEntity = new ToDoEntity();
      var connectionString = _configuration.GetConnectionString("MongoConnection");
      var table = TableNames.todoobject.ToString();
      var userId = request.UserId;
      var title = request.Title;
      var description = request.Description;
      var priority = request.Priority;
      var newTitle = request.NewTitle;

      try
      {
        await _dBInterface.Update<ToDoEntity>(userId, title, description, priority, newTitle);

        // Get the newly updated ToDoObject
        if (!string.IsNullOrEmpty(newTitle))
        {
          updatedEntity = await _dBInterface.Read(userId, newTitle);
        }
        else
        {
          updatedEntity = await _dBInterface.Read(userId, title);
        }

        _logger.LogInformation("Successfully updated ToDoObject");
      }
      catch (Exception ex)
      {
        _logger.LogError("Could not find the given ToDoObject", ex);
      }

      return updatedEntity;
    }
  }
}
