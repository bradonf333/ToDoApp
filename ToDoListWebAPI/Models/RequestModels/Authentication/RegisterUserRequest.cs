using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebAPI.Helpers;

namespace ToDoListWebAPI.Models.RequestModels.Authentication
{
  public class RegisterUserRequest
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public UserRoles AccountRoles { get; set; }
    public string UserId { get; set; }
  }
}
