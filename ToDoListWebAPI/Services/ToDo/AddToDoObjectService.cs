using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;

namespace ToDoListWebAPI.Services.ToDo
{
  public class AddToDoObjectService : IAddToDoObjectService
  {
    private readonly IDBInterface _dBInterface;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AddToDoObjectService> _logger;

    public AddToDoObjectService(
      IDBInterface dBInterface,
      IConfiguration configuration,
      ILogger<AddToDoObjectService> logger)
    {
      _dBInterface = dBInterface;
      _configuration = configuration;
      _logger = logger;
    }

    public async Task<T> AddToDoObject<T>(ToDoEntity toDoEntity)
      where T : class
    {
      ToDoEntity result = null;

      try
      {
        result = await _dBInterface.CreateAsync<ToDoEntity>(toDoEntity);
        _logger.LogInformation("Successfully created new ToDoObject");
      }
      catch (Exception ex)
      {
        _logger.LogError("Failed to create the ToDoObject", ex);
      }

      return result as T;
    }
  }
}
