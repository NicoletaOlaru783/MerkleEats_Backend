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
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Menu> CreateAsync(Menu menu)
        {
            var result = await _db.Menus.AddAsync(menu);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Menu> DeleteAsync(int id)
        {
            var result = await _db.Menus.FirstOrDefaultAsync(e => e.Id != id );
            _db.Menus.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Menu>> ReadAllAsync()
        {
            return await _db.Menus.OrderBy(a => a.CreatedAt).ToListAsync();
        }
    }
}
