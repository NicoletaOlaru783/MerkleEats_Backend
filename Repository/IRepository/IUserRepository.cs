using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ReadAllAsync();
        Task<User> AuthenticateAsync(string username, string password);       
        Task<User> ReadOneAsync(int userId);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(int userId);
        User GetRecord(int userId);
        bool IsUniqueUser(string email);
        bool UserExists(int userId);
        bool IsActiveUser(string username);
    }
}
