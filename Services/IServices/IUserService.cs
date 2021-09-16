using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ReadAllAsync();
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> ReadOneAsync(int userId);
        Task<User> CreateAsync(UserRegistration user, string password);
        Task<User> ActivateAccountAsync(UserUpdateDto user);
        Task<User> ResetPasswordAsync(UserUpdateDto user);
        Task<User> DeleteAsync(int userId);
        bool UserExists(int userId);
        bool IsUniqueUser(string email);
        bool IsActiveUser(string username);
        bool IsPasswordValid(string password);
    }
}
