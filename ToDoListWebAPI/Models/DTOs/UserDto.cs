using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebAPI.Helpers;

namespace ToDoListWebAPI.Models.DTOs
{
  public class UserDto
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public UserRoles AccountRoles { get; set; }
    public string UserId { get; set; }
  }
}
