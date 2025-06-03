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
                ProductList = entity.OrderItems
                    .Select(itemEntity =>  new ProductBaseResponse
                    {
                        Id = itemEntity.Product.Id,
                        Name = itemEntity.Product.Name,
                        Description = itemEntity.Product.Description,
                        Price = itemEntity.Product.Price
                    }).ToList(),
                TotalPrice = entity.TotalPrice
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

        public static OrderEntity ToEntity(OrderPayload payload)
        {
            return new OrderEntity
            {
                OrderItems = payload.BurgerList.Select(basePayload => new OrderItemEntity
                {
                    ProductId = basePayload.ProductId,
                }).ToList()
            };
        }
    }
}
