using MerkleKitchenApp_V2.Data;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<OrderItem>> ReadAllAsync()
        {
            return await _db.OrderItems.Include(c => c.Order).OrderBy(a => a.CreatedAt).ToListAsync();
        }

        public async Task<OrderItem> ReadOneAsync(int orderItemId)
        {
            return await _db.OrderItems.Include(c => c.Order).FirstOrDefaultAsync(a => a.Id == orderItemId);
        }


        public bool OrderItemExists(int orderItemId)
        {
            return _db.OrderItems.Any(a => a.Id == orderItemId);

        }

        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            var result = await _db.OrderItems.AddAsync(orderItem);
            await _db.SaveChangesAsync();
            return result.Entity;
        }


        public async Task<OrderItem> DeleteAsync(int orderItemId)
        {
            var result = await _db.OrderItems.FirstOrDefaultAsync(e => e.Id == orderItemId);
            _db.OrderItems.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }


        public async Task<IEnumerable<OrderItem>> ReadOneOrderIdAsync(int orderId)
        {
            return await _db.OrderItems.Include(c => c.Order).Where(c => c.OrderId == orderId).ToListAsync();
        }

       

        
    }
}
