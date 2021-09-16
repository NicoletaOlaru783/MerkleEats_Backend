using MerkleKitchenApp_V2.Data;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Product>> ReadAllAsync()
        {
            return await _db.Products.OrderBy(a => a.CreatedAt).ToListAsync();
        }

        public async Task<Product> ReadOneAsync(int productId)
        {
            return await _db.Products.FirstOrDefaultAsync(a => a.Id == productId);
        }
        

        public bool ProductExists(int productId)
        {
            return _db.Products.Any(a => a.Id == productId);
            
        }

        public bool ProductExists(string name)
        {
            return _db.Products.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            
        }


        public async Task<Product> CreateAsync(Product product)
        {
            var result = await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            var result = await _db.Products.FirstOrDefaultAsync(e => e.Id == productId);
            _db.Products.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public Product GetRecord(int productId)
        {
            return _db.Products.FirstOrDefault(a => a.Id == productId);
        }
    }
}
