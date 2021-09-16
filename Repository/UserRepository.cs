using AutoMapper;
using MerkleKitchenApp_V2.Data;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var result = await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
            return result;
        }

        public async Task<IEnumerable<User>> ReadAllAsync()
        {
            return await _db.Users.OrderBy(a => a.CreatedAt).ToListAsync();
        }

        public async Task<User> ReadOneAsync(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(e => e.Id == userId);
        }

        public bool IsUniqueUser(string email)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == email);

            if (user == null)
                return true;

            return false;
        }

        public async Task<User> CreateAsync(User user)
        {
            var result = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return result.Entity;
        }              
               

        public async Task<User> UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(int userId)
        {
            var result = await _db.Users.FirstOrDefaultAsync(e => e.Id == userId);
            _db.Users.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public bool UserExists(int userId)
        {
            return _db.Users.Any(a => a.Id == userId);
        }

        public bool IsActiveUser(string username)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username);

            if (user.IsActive == true)
                return true;

            return false;
        }

        public User GetRecord(int userId)
        {
            return _db.Users.FirstOrDefault(e => e.Id == userId);
        }
    }
}

