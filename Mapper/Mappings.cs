using AutoMapper;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<User, UserAuthentication>().ReverseMap();
            CreateMap<User, UserRegistration>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
            CreateMap<Order, OrderConfirmDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
        }
    }
}
