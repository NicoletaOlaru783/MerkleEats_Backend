using MerkleKitchenApp_V2.Data;
using MerkleKitchenApp_V2.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository.IRepository
{
    public class TvRepository : ITvRepository
    {
        private readonly ApplicationDbContext _db;

        public TvRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TV>> ReadAllAsync()
        {
            return await _db.Settings.OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<Instagram> ReadOneAsync(int id)
        {
            return await _db.Instagram.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<TV> UpdateAsync(TV tv)
        {
            _db.Settings.Update(tv);
            await _db.SaveChangesAsync();
            return tv;
        }

        public async Task<Instagram> UpdateAsync(Instagram instagram)
        {
            _db.Instagram.Update(instagram);
            await _db.SaveChangesAsync();
            return instagram;
        }
    }
}
