using AutoMapper;

namespace Application.Services.User.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserPayload>();
            CreateMap<UserPayload, User>();
        }
    }
}