using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels;
using ToDoListWebAPI.Models.ResponseModels;
using ToDoListWebAPI.Services.ToDo;

namespace ToDoListWebAPI.Controllers
{
  [Route("api/ToDoObject")]
  public class ToDoObjectController : Controller
  {
    private readonly IGetToDoObjectService _getToDoObjectService;
    private readonly IAddToDoObjectService _addToDoObjectService;
    private readonly IDeleteToDoObjectService _deleteToDoObjectService;
    private readonly IUpdateToDoObjectService _updateToDoObjectService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ToDoObjectController(
      IGetToDoObjectService getToDoObjectService,
      IAddToDoObjectService addToDoObjectService,
      IDeleteToDoObjectService deleteToDoObjectService,
      IUpdateToDoObjectService updateToDoObjectService,
      IConfiguration configuration,
      IMapper mapper)
    {
      _getToDoObjectService = getToDoObjectService;
      _addToDoObjectService = addToDoObjectService;
      _deleteToDoObjectService = deleteToDoObjectService;
      _updateToDoObjectService = updateToDoObjectService;
      _configuration = configuration;
      _mapper = mapper;
    }

    // GET: api/<controller>
    [HttpGet("allTodoForUser")]
    public async Task<IEnumerable<GetToDoObjectResponse>> GetAllForUser(GetAllToDoEntityRequest request)
    {
      var result = await _getToDoObjectService.GetAllToDoObjectsForUser(request.UserId);

      return _mapper.Map<List<ToDoEntity>, List<GetToDoObjectResponse>>(result);
    }

    // GET api/<controller>/
    [HttpGet("todo")]
    public async Task<GetToDoObjectResponse> Get(GetToDoObjectRequest request)
    {
      var result = await _getToDoObjectService.GetToDoObject(request);

      return _mapper.Map<GetToDoObjectResponse>(result);
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<AddToDoObjectResponse> Post([FromBody] AddToDoObjectRequest request)
    {
      var toDo = _mapper.Map<ToDoEntity>(request);
      var result = await _addToDoObjectService.AddToDoObject<ToDoEntity>(toDo);
      return _mapper.Map<AddToDoObjectResponse>(result);
    }

    // PUT api/<controller>/5
    [HttpPut]
    public async Task<UpdateToDoObjectResponse> Update(UpdateToDoObjectRequest request)
    {
      var updatedToDoEntity = await _updateToDoObjectService.UpdateToDoObject(request);

      return _mapper.Map<UpdateToDoObjectResponse>(updatedToDoEntity);
    }

    // DELETE api/<controller>/5
    [HttpDelete]
    public async Task<DeleteToDoObjectResponse> Delete(GetToDoObjectRequest request)
    {
      var toDoObject = await _getToDoObjectService.GetToDoObject(request);
      var result = await _deleteToDoObjectService.DeleteToDoObject<DeleteToDoObjectResponse>(toDoObject);

      return result;
    }
  }
}
