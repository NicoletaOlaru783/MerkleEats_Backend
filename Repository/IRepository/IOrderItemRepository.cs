using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository.IRepository
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> ReadAllAsync();
        Task<OrderItem> ReadOneAsync(int orderItemId);
        Task<OrderItem> CreateAsync(OrderItem orderItem);
        Task<OrderItem> DeleteAsync(int orderItemId);
        bool OrderItemExists(int orderItemId);

        //Links
        Task<IEnumerable<OrderItem>> ReadOneOrderIdAsync(int orderId);
    }
}
