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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;
        private IEmailService _emailService;

        public OrderService(IOrderRepository orderRepo, IMapper mapper, IEmailService emailService)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _emailService = emailService;
        }

        //Methods
        public async Task<IEnumerable<OrderDto>> ReadAllAsync()
        {
            var results = await _orderRepo.ReadAllAsync();
            var objDto = new List<OrderDto>();
            foreach (var obj in results)
            {
                objDto.Add(_mapper.Map<OrderDto>(obj));
            }
            return objDto;
        }

        public async Task<OrderDto> ReadOneAsync(int orderId)
        {
            var result = await _orderRepo.ReadOneAsync(orderId); ;
            var resultDto = _mapper.Map<OrderDto>(result);
            return resultDto;
        }

        public async Task<Order> CreateAsync(OrderCreateDto order)
        {
            var result = _mapper.Map<Order>(order);
            result.Price = 0;
            result.Status = "New";
            result.CreatedAt = DateTime.Now;
            var guid = Guid.NewGuid().ToString();
            result.UID = guid;
            var orderCreated = await _orderRepo.CreateAsync(result);
            // _emailService.SendConfirmOrderEmail(result.Email, result.UID);

            return orderCreated;
        }

        public async Task<List<Order>> UpdateAsync(List<OrderUpdateDto> order)
        {
            var objDto = new List<Order>();
            foreach (var obj in order)
            {
                objDto.Add(_mapper.Map<Order>(obj));
                var objectToUpdate = _orderRepo.GetRecord(obj.Id);
                objectToUpdate.Status = obj.Status;
                objectToUpdate.Price = obj.Price;
                objectToUpdate.KitchenComment = obj.KitchenComment;
                await _orderRepo.UpdateAsync(objectToUpdate);
            }

            return objDto;
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            return await _orderRepo.DeleteAsync(orderId);
        }

        public bool OrderExists(int orderId)
        {
            return _orderRepo.OrderExists(orderId);
        }

        public async Task<IEnumerable<OrderDto>> ReadByDateAsync(DateTime createdAt)
        {
            var results = await _orderRepo.ReadByDateAsync(createdAt);
            var objDto = new List<OrderDto>();
            foreach (var obj in results)
            {
                objDto.Add(_mapper.Map<OrderDto>(obj));
            }
            return objDto;
        }

        public async Task<Order> ConfirmOrder(string UID)
        {
            var objectToUpdate = _orderRepo.GetRecordbyEmail(UID);
            objectToUpdate.ConfirmedAt = DateTime.Now;
            return await _orderRepo.UpdateAsync(objectToUpdate);
        }

        public async Task<Order> CancelOrder(string UID)
        {
            var objectToUpdate = _orderRepo.GetRecordbyEmail(UID);
            objectToUpdate.ConfirmedAt = null;
            return await _orderRepo.UpdateAsync(objectToUpdate);
        }
    }
}
