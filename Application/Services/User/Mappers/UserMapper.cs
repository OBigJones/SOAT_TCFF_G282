using Application.Services.User.Payload;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.User.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserEntity, UserPayload>();
            CreateMap<UserPayload, UserEntity>();
        }
    }
}