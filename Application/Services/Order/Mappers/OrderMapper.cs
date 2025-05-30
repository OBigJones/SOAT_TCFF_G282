using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Order.Mappers
{
    public class OrderMapper : Profile
    {
        OrderMapper()
        {
            CreateMap<OrderEntity, OrderPayload>();
            CreateMap<OrderPayload, OrderEntity>();
            CreateMap<OrderEntity, OrderResponse>();
            CreateMap<OrderResponse, OrderEntity>();
        }
    }
}
