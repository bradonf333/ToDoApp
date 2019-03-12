using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebAPI.Models.RequestModels
{
  public class GetAllToDoEntityRequest
  {
    public string UserId { get; set; }
  }
}