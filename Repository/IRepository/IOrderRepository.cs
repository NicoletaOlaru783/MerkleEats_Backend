using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> ReadAllAsync();
        Task<IEnumerable<Order>> ReadByDateAsync(DateTime createdAt);
        Task<Order> ReadOneAsync(int orderId);
        bool OrderExists(int orderId);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<Order> DeleteAsync(int orderId);
        Order GetRecord(int orderId);
        Order GetRecordbyEmail(string UID);
    }
}
