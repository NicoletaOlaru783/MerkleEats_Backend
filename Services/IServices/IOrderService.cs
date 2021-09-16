using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> ReadAllAsync();
        Task<OrderDto> ReadOneAsync(int orderId);
        Task<IEnumerable<OrderDto>> ReadByDateAsync(DateTime createdAt);
        Task<Order> CreateAsync(OrderCreateDto order);
        Task<List<Order>> UpdateAsync(List<OrderUpdateDto> order);
        Task<Order> ConfirmOrder(string UID);
        Task<Order> CancelOrder(string UID);
        Task<Order> DeleteAsync(int orderId);
        bool OrderExists(int orderId);
    }
}
