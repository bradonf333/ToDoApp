using Framework.Db.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebAPI.Helpers;
using ToDoListWebAPI.Models;
using ToDoListWebAPI.Models.EntityModels;

namespace ToDoListWebAPI.Services
{
  public class MongoDbActionService : IDBInterface
  {
    private readonly IDbOperations<ToDoEntity> _userDbOperations;
    private readonly ILogger<MongoDbActionService> _logger;
    private IMongoDatabase _db;

    public MongoDbActionService(IOptions<ToDoConfig> config, ILogger<MongoDbActionService> logger, IDbOperations<ToDoEntity> userDbOperations)
    {
      _userDbOperations = userDbOperations;
      _userDbOperations.InitializeDb(config.Value.ConnectionString, config.Value.DatabaseName, config.Value.CollectionName);
      _logger = logger;
    }

    async Task<T> IDBInterface.CreateAsync<T>(ToDoEntity todo)
    {
      if ((await _userDbOperations.GetAllAsync(e => e.UserId == todo.UserId && e.Title == todo.Title)).Any())
      {
        throw new AppException("ToDo Item with Title  \"" + todo.Title + "\" is already taken");
      }

      try
      {
        await _userDbOperations.CreateAsync(todo).ConfigureAwait(false);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }

      // Get the newly added ToDo
      var result = await _userDbOperations.GetAsync(e => e.UserId == todo.UserId && e.Title == todo.Title).ConfigureAwait(false);

      return result as T;
    }

    public async Task<ToDoEntity> Read(string userId, string title)
    {
      try
      {
        var result = await _userDbOperations.GetAsync(e => e.UserId == userId && e.Title == title).ConfigureAwait(false);
        return result;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

      return null;
    }

    public async Task<List<ToDoEntity>> ReadAllByUserId(string userId)
    {
      try
      {
        var result = await _userDbOperations.GetAllAsync(e => e.UserId == userId).ConfigureAwait(false);
        return result;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

      return null;
    }

    public async Task Update<T>(string userId, string existingTitle, string description, string priority, string newTitle)
      where T : class
    {
      var collectionName = CollectionName.todo.ToString();
      var collection = _db.GetCollection<ToDoEntity>(collectionName);

      var filter = Builders<ToDoEntity>.Filter.Eq("UserId", userId)
                   & Builders<ToDoEntity>.Filter.Eq("Title", existingTitle);

      // Initialize this update statement with a field required in order to even find the object.
      var updateStatement = Builders<ToDoEntity>.Update.Set("UserId", userId);

      if (!string.IsNullOrEmpty(description))
      {
        updateStatement = Builders<ToDoEntity>.Update.Set("Description", description);
      }

      if (!string.IsNullOrEmpty(priority))
      {
        updateStatement = updateStatement.Set("Priority", priority);
      }

      if (!string.IsNullOrEmpty(newTitle))
      {
        updateStatement = updateStatement.Set("Title", newTitle);
      }

      try
      {
        var result = await collection.UpdateOneAsync(filter, updateStatement);
        if (!result.IsAcknowledged)
        {
          throw new Exception($"{result}");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }

    public async Task Delete<T>(string connectionString, string table, string userId, string title)
      where T : class
    {
      var client = new MongoClient(connectionString);
      var database = client.GetDatabase(table);

      // No ToDoEntity here, so using the Enum.
      var collectionName = CollectionName.todo.ToString();
      var collection = database.GetCollection<T>(collectionName);

      var filter = Builders<T>.Filter.Eq("UserId", userId)
                   & Builders<T>.Filter.Eq("Title", title);

      try
      {
        var result = await collection.DeleteOneAsync(filter);
        if (!result.IsAcknowledged)
        {
          throw new Exception($"{result}");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }
}
