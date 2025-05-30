using Application.Services.User.Payload;
using Domain.Entities;

namespace Application.Services.User.Mappers
{
    public static class UserMapper
    {
        public static UserPayload ToPayload(UserEntity entity)
        {
            return new UserPayload
            {
                Name = entity.Nome,
                CPF = entity.CPF,
                Email = entity.Email
            };
        }

        public static UserEntity ToEntity(UserPayload payload)
        {
            return new UserEntity
            {
                Nome = payload.Name,
                CPF = payload.CPF,
                Email = payload.Email
            };
        }
    }
}