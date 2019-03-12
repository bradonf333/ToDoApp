﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebAPI.Models.ResponseModels
{
    public class GetToDoObjectResponse
    {
      public string Description { get; set; }
      public string Title { get; set; }
      public string Status { get; set; }
      public string Priority { get; set; }
    }
}
