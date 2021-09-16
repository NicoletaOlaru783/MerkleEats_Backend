using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ReadAllAsync();
        Task<Product> ReadOneAsync(int productId);
        bool ProductExists(string name);
        bool ProductExists(int productId);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(int productId);
        Product GetRecord(int productId);

    }
}
