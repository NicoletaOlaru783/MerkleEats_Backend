using MerkleKitchenApp_V2.Data;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Order>> ReadAllAsync()
        {
            return await _db.Orders.OrderBy(a => a.CreatedAt).ToListAsync();
        }

        public async Task<Order> ReadOneAsync(int orderId)
        {
            return await _db.Orders.FirstOrDefaultAsync(a => a.Id == orderId);
        }


        public bool OrderExists(int orderId)
        {
            return _db.Orders.Any(a => a.Id == orderId);

        }

        public async Task<Order> CreateAsync(Order order)
        {
            var result = await _db.Orders.AddAsync(order);
            //Thread.Sleep(10000);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            var result = await _db.Orders.FirstOrDefaultAsync(e => e.Id == orderId);
            _db.Orders.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public Order GetRecord(int orderId)
        {
            return _db.Orders.FirstOrDefault(a => a.Id == orderId);
        }

        public async Task<IEnumerable<Order>> ReadByDateAsync(DateTime createdAt)
        {
            return await _db.Orders.Where(c => c.CreatedAt >= createdAt).OrderByDescending(a => a.CreatedAt).ToListAsync();
        }

        public Order GetRecordbyEmail(string UID)
        {
            return _db.Orders.FirstOrDefault(a => a.UID == UID);
        }
    }
}
