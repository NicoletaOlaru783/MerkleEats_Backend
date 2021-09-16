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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        //Methods
        public async Task<IEnumerable<ProductDto>> ReadAllAsync()
        {
            var results = await _productRepo.ReadAllAsync();
            //_mapper.Map<ProductDto>( (results); look into, maybe remove the foreach

            var objDto = new List<ProductDto>();
            foreach (var obj in results)
            {
                objDto.Add(_mapper.Map<ProductDto>(obj));
            }
            return objDto;
        }

        public async Task<ProductDto> ReadOneAsync(int productId)
        {
            var result = await _productRepo.ReadOneAsync(productId); ;
            var resultDto = _mapper.Map<ProductDto>(result);
            return resultDto;
        }

        public async Task<Product> CreateAsync(ProductCreateDto product)
        {
            var result = _mapper.Map<Product>(product);
            result.CreatedAt = DateTime.Now;
            return await _productRepo.CreateAsync(result);
        }

        public async Task<Product> UpdateAsync(ProductDto product)
        {
            var objectToUpdate = _productRepo.GetRecord(product.Id);
            objectToUpdate.Name = product.Name;
            objectToUpdate.Description = product.Description;
            return await _productRepo.UpdateAsync(objectToUpdate);
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            return await _productRepo.DeleteAsync(productId);
        }

        public bool ProductExists(int productId)
        {
            return _productRepo.ProductExists(productId);
        }
    }
}
