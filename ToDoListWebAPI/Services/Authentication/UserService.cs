using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Db.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using ToDoListWebAPI.Helpers;
using ToDoListWebAPI.Models.EntityModels;

namespace ToDoListWebAPI.Services.Authentication
{
  public interface IUserService
  {
    Task<User> Authenticate(string emailAddress, string password);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(string id);
    Task<User> GetByUserId(string userId);
    Task Create(User user, string password);
    Task Update(User user, string password = null);
    Task Delete(string id);
    Task DeleteAll();
  }

  public class UserService : IUserService
  {
    private readonly IDbOperations<User> _userDbOperations;
    private readonly IPasswordHashService _passwordHashService;
    private readonly ILogger<UserService> _logger;

    public UserService(IOptions<UserConfig> config, ILogger<UserService> logger, IDbOperations<User> userDbOperations, IPasswordHashService passwordHashService)
    {
      _userDbOperations = userDbOperations;
      _passwordHashService = passwordHashService;
      _userDbOperations.InitializeDb(config.Value.ConnectionString, config.Value.DatabaseName, config.Value.CollectionName);
      _logger = logger;
    }

    public async Task<User> Authenticate(string userId, string password)
    {
      if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
      {
        return null;
      }

      var user = await _userDbOperations.GetAsync(e => e.UserId == userId).ConfigureAwait(false);

      // check if userId exists
      if (user == null)
      {
        return null;
      }

      // check if password is correct
      if (!_passwordHashService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
      {
        return null;
      }

      // authentication successful
      return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
      return await _userDbOperations.GetAllAsync().ConfigureAwait(false);
    }

    public async Task<User> GetById(string id)
    {
      var objectId = new ObjectId(id);
      return await _userDbOperations.GetAsync(e => e.Id == objectId).ConfigureAwait(false);
    }

    public async Task<User> GetByUserId(string userId)
    {
      return await _userDbOperations.GetAsync(e => e.UserId == userId).ConfigureAwait(false);
    }

    public async Task Create(User user, string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new AppException("Password is required");
      }

      if ((await _userDbOperations.GetAllAsync(e => e.UserId == user.UserId)).Any())
      {
        throw new AppException("UserId \"" + user.UserId + "\" is already taken");
      }

      _passwordHashService.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await _userDbOperations.CreateAsync(user).ConfigureAwait(false);
    }

    public async Task Update(User userParam, string password)
    {
      var user = await GetById(userParam.Id.ToString());

      if (user == null)
      {
        throw new AppException("User not found");
      }

      if (userParam.UserId != user.UserId)
      {
        // userId has changed so check if the new userId is already taken
        if ((await _userDbOperations.GetAllAsync(e => e.UserId == userParam.UserId)).Any())
        {
          throw new AppException("UserId " + userParam.UserId + " is already taken");
        }
      }

      // update user properties
      user.FirstName = userParam.FirstName;
      user.LastName = userParam.LastName;
      user.UserId = userParam.UserId;

      // update password if it was entered
      if (!string.IsNullOrWhiteSpace(password))
      {
        _passwordHashService.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      await _userDbOperations.UpdateAsync(e => e.Id == userParam.Id, user);
    }

    public async Task Delete(string id)
    {
      await _userDbOperations.RemoveAsync(e => e.Id == new ObjectId(id));
    }

    public async Task DeleteAll()
    {
      await _userDbOperations.RemoveAllAsync();
    }
  }
}
