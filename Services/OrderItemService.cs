using AutoMapper;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Repository.IRepository;
using MerkleKitchenApp_V2.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepo, IMapper mapper)
        {
            _orderItemRepo = orderItemRepo;
            _mapper = mapper;
        }

        //Methods
        public async Task<IEnumerable<OrderItemDto>> ReadAllAsync()
        {
            var results = await _orderItemRepo.ReadAllAsync();
            var objDto = new List<OrderItemDto>();
            foreach (var obj in results)
            {
                objDto.Add(_mapper.Map<OrderItemDto>(obj));
            }
            return objDto;
        }

        public async Task<OrderItemDto> ReadOneAsync(int orderItemId)
        {
            var result = await _orderItemRepo.ReadOneAsync(orderItemId); ;
            var resultDto = _mapper.Map<OrderItemDto>(result);
            return resultDto;
        }

        public async Task<List<OrderItem>> CreateAsync(List<OrderItemCreateDto> orderItem)
        {
            var objDto = new List<OrderItem>();
            foreach (var obj in orderItem)
            {
                objDto.Add(_mapper.Map<OrderItem>(obj));
                var objToCreate = _mapper.Map<OrderItem>(obj);
                objToCreate.CreatedAt = DateTime.Now;
                await _orderItemRepo.CreateAsync(objToCreate);
            }            
            return objDto;
        }


        public async Task<OrderItem> DeleteAsync(int orderItemId)
        {
            return await _orderItemRepo.DeleteAsync(orderItemId);
        }

        public bool OrderItemExists(int orderItemId)
        {
            return _orderItemRepo.OrderItemExists(orderItemId);
        }

        public async Task<IEnumerable<OrderItemDto>> ReadOneOrderIdAsync(int orderId)
        {
            var results = await _orderItemRepo.ReadOneOrderIdAsync(orderId);
            var objDto = new List<OrderItemDto>();
            foreach (var obj in results)
            {
                objDto.Add(_mapper.Map<OrderItemDto>(obj));
            }
            return objDto;
        }

    }
}
