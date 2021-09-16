using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> ReadAllAsync();
        Task<List<Menu>> CreateAsync(List<Menu> menu);
        Task DeleteAsync(List<Menu> menu);
    }
}
