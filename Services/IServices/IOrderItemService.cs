using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> ReadAllAsync();
        Task<OrderItemDto> ReadOneAsync(int orderItemId);
        Task<List<OrderItem>> CreateAsync(List<OrderItemCreateDto> orderItem);
        Task<OrderItem> DeleteAsync(int orderItemId);
        bool OrderItemExists(int orderItemId);

        //Links
        Task<IEnumerable<OrderItemDto>> ReadOneOrderIdAsync(int orderId);
    }
}
