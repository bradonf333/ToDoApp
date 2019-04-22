using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ToDoListWebAPI.Models;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public class GetToDoObjectService : IGetToDoObjectService
  {
    private readonly IDBInterface _dBInterface;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GetToDoObjectService> _logger;

    public GetToDoObjectService(
      IDBInterface dBInterface,
      IConfiguration configuration,
      ILogger<GetToDoObjectService> logger)
    {
      _dBInterface = dBInterface;
      _configuration = configuration;
      _logger = logger;
    }

    public async Task<ToDoEntity> GetToDoObject(GetToDoObjectRequest request)
    {
      ToDoEntity toDoEntity = null;
      var userId = request.UserId;
      var title = request.Title;

      try
      {
        toDoEntity = await _dBInterface.Read(userId, title);

        _logger.LogInformation("Successfully found ToDoObject");
      }
      catch (Exception ex)
      {
        _logger.LogError("Could not find the given ToDoObject", ex);
      }

      return toDoEntity;
    }

    public async Task<List<ToDoEntity>> GetAllToDoObjectsForUser(string userId)
    {
      List<ToDoEntity> toDoObjects = null;

      try
      {
        toDoObjects = await _dBInterface.ReadAllByUserId(userId);
      }
      catch (Exception ex)
      {
        _logger.LogError("Could not return the list of ToDo objects for the given user", ex);
      }

      return toDoObjects;
    }
  }
}
