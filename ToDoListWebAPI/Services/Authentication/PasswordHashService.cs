using Microsoft.Extensions.Logging;
using System;

namespace ToDoListWebAPI.Services.Authentication
{
  public interface IPasswordHashService
  {
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
  }

  public class PasswordHashService : IPasswordHashService
  {
    private ILogger<PasswordHashService> _logger;

    public PasswordHashService(ILogger<PasswordHashService> logger)
    {
      _logger = logger;
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null)
      {
        throw new ArgumentNullException(nameof(password));
      }
      if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password)); }

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null)
      {
        throw new ArgumentNullException(nameof(password));
      }

      if (string.IsNullOrWhiteSpace(password))
      {
        throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
      }

      if (storedHash.Length != 64)
      {
        var exceptionMessage = "Invalid length of password hash (64 bytes expected).";
        _logger.LogError($"{exceptionMessage} hash Length: {storedHash.Length}");
        throw new ArgumentException(exceptionMessage, "passwordHash");
      }

      if (storedSalt.Length != 128)
      {
        var exceptionMessage = "Invalid length of password salt (128 bytes expected).";
        _logger.LogError($"{exceptionMessage} salt length: {storedSalt.Length}");
        throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");
      }

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i])
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}
