using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface ITvService         
    {
        Task<IEnumerable<TV>> ReadAllAsync();
        Task<TV> UpdateAsync(TV tv);
        Task<Instagram> ReadOneAsync(int id);
        Task<Instagram> UpdateAsync(Instagram instagram);
    }
}
