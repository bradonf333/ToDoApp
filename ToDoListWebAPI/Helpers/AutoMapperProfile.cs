using AutoMapper;
using ToDoListWebAPI.Models.DTOs;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Models.RequestModels;
using ToDoListWebAPI.Models.RequestModels.Authentication;
using ToDoListWebAPI.Models.ResponseModels;

namespace ToDoListWebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RegisterUserRequest, User>();
            CreateMap<ToDoEntity, GetToDoObjectResponse>();
            CreateMap<AddToDoObjectRequest, ToDoEntity>();
            CreateMap<AddToDoObjectResponse, ToDoEntity>();
        }
    }
}
