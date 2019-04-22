using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoListWebAPI.Helpers;
using ToDoListWebAPI.Models.DTOs;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels.Authentication;
using ToDoListWebAPI.Services.Authentication;

namespace ToDoListWebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly ILogger<UserController> _logger;

    public UserController(
      IUserService userService,
      IMapper mapper,
      IOptions<AppSettings> appSettings,
      ILogger<UserController> logger)
    {
      _userService = userService;
      _mapper = mapper;
      _logger = logger;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]AuthenticateUserRequest userDto)
    {
      var user = await _userService.Authenticate(userDto.UserId, userDto.Password);

      if (user == null)
      {
        return BadRequest(new { message = "UserId or password is incorrect" });
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.UserData, user.UserId), // TODO: Not sure what to do for this Claim?
                    new Claim(ClaimTypes.Role, ((UserRoles)user.AccountRoles).ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info (without password) and token to store client side
      return Ok(new
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString,
        UserId = user.UserId,
        UserRoles = user.AccountRoles
      });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserRequest userDto)
    {
      // map dto to entity
      var user = _mapper.Map<User>(userDto);

      try
      {
        // save 
        await _userService.Create(user, userDto.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var users = await _userService.GetAll();
      var userDtos = _mapper.Map<IList<UserDto>>(users);
      return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
      var user = await _userService.GetByUserId(userId);
      var userDto = _mapper.Map<UserDto>(user);
      return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody]UserDto userDto)
    {
      // map dto to entity and set id
      var user = _mapper.Map<User>(userDto);
      user.Id = new ObjectId(id);

      try
      {
        // save 
        await _userService.Update(user, userDto.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      await _userService.Delete(id);
      return Ok();
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteAll()
    {
      await _userService.DeleteAll();
      return Ok();
    }
  }
}