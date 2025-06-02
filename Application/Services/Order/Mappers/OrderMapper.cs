using Application.Services.Order.Payload;
using Application.Services.Order.Response;
using Domain.Entities;

namespace Application.Services.Order.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponse ToResponse(OrderEntity entity)
        {
            return new OrderResponse
            {
                OrderCode = entity.OrderCode,
                CustomerName = entity.User?.Nome ?? "Unknown",
                ProductList = entity.ProductList
                    .Select(productEntity => new ProductBasePayload
                    {
                        Id = productEntity.Id,
                        Name = productEntity.Name,
                        Description = productEntity.Description,
                        Price = productEntity.Price
                    }).ToList()
            };
        }

        public static List<OrderResponse> ToResponseList(List<OrderEntity> entities)
        {
            var responses = new List<OrderResponse>();
            foreach (var entity in entities)
            {
                responses.Add(ToResponse(entity));
            }

            return responses;
        }

        public static OrderPayload ToPayload(OrderEntity entity)
        {
            return new OrderPayload
            {
                Id = entity.Id,
                CustomerName = entity.User?.Nome,
                OrderCode = entity.OrderCode,
                BurgerList = entity.ProductList
                    .Select(productEntity => new ProductBasePayload
                    {
                        Id = productEntity.Id,
                        Name = productEntity.Name,
                        Description = productEntity.Description,
                        Price = productEntity.Price,
                    }).ToList(),
                TotalPrice = entity.TotalPrice,
                Status = entity.Status,
                Expiration = entity.Expiration
            };
        }

        public static OrderEntity ToEntity(OrderPayload payload)
        {
            return new OrderEntity
            {
                Id = payload.Id,
                OrderCode = payload.OrderCode,
                ProductList = payload.BurgerList.Select(basePayload => new ProductEntity
                {
                    Id = basePayload.Id,
                    Name = basePayload.Name,
                    Description = basePayload.Description,
                    Price = basePayload.Price,
                    Quantity = 1,
                }).ToList()
            };
        }
    }
}
