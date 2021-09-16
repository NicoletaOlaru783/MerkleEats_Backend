using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> ReadAllAsync();
        Task<ProductDto> ReadOneAsync(int productId);
        Task<Product> CreateAsync(ProductCreateDto product);
        Task<Product> UpdateAsync(ProductDto product);
        Task<Product> DeleteAsync(int productId);
        bool ProductExists(int productId);
    }
}
