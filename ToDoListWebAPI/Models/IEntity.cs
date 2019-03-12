using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebAPI.Models
{
  public interface IEntity
  {
    string TableName { get; set; }
  }
}