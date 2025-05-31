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
                OrderCode = entity.OrderCode
            };
        }

        public static OrderPayload ToPayload(OrderEntity entity)
        {
            return new OrderPayload
            {
                Id = entity.Id,
                CustomerName = entity.User?.Nome,
                OrderCode = entity.OrderCode,
                BurgerList = entity.BurgerList
                    .Select(burgerEntity => new ProducBasePayload
                    {
                        Id = burgerEntity.Id,
                        Name = burgerEntity.Name,
                        Description = burgerEntity.Description,
                        Price = burgerEntity.Price,
                    }).ToList(),
                Beverages = entity.Beverages
                    .Select(beverageEntity => new ProducBasePayload
                    {
                        Id = beverageEntity.Id,
                        Name = beverageEntity.Name,
                        Description = beverageEntity.Description,
                        Price = beverageEntity.Price
                    }).ToList(),
                Desserts = entity.Desserts
                    .Select(dessertEntity => new ProducBasePayload
                    {
                        Id = dessertEntity.Id,
                        Name = dessertEntity.Name,
                        Description = dessertEntity.Description,
                        Price = dessertEntity.Price
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
                BurgerList = payload.BurgerList.Select(basePayload => new BurgerEntity
                {
                    Id = basePayload.Id,
                    Name = basePayload.Name,
                    Description = basePayload.Description,
                    Price = basePayload.Price
                }).ToList(),
                Beverages = payload.Beverages.Select(basePayload => new BeverageEntity
                {
                    Id = basePayload.Id,
                    Name = basePayload.Name,
                    Description = basePayload.Description,
                    Price = basePayload.Price
                }).ToList(),
                Desserts = payload.Desserts.Select(basePayload => new DessertEntity
                {
                    Id = basePayload.Id,
                    Name = basePayload.Name,
                    Description = basePayload.Description,
                    Price = basePayload.Price
                }).ToList()
            };
        }
    }
}
