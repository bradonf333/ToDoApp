﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebAPI.Models.ResponseModels
{
  public class AddToDoObjectResponse
  {
    public string Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Title { get; set; }
  }
}
