using Application.Repository;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context) => _context = context;

        public async Task<bool> CreateOrderAsync(OrderEntity order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<OrderEntity>> GetActiveOrders()
        {
            return await _context.Orders
                                 .Where(o => o.Status == OrderStatus.Received)
                                 .Include(o => o.OrderItems)
                                 .ToListAsync();
        }

        public async Task<List<OrderEntity>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                                 .Where(o => o.Status == status)
                                 .Include(o => o.OrderItems)
                                 .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(string orderCode, OrderStatus newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderCode == orderCode);
            if (order == null)
            {
                return false; // Order not found
            }
            order.Status = newStatus;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderEntity?> GetOrderByCode(string orderCode)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderCode == orderCode);
            if (order == null)
            {
                return null;
            }
            
            return order;
        }
    }
}
