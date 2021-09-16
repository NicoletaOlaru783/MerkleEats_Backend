using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository.IRepository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> ReadAllAsync();
        Task<Menu> CreateAsync(Menu menu);
        Task<Menu> DeleteAsync(int id);
    }
}
