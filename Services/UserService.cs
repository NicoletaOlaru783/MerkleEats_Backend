using AutoMapper;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Repository.IRepository;
using MerkleKitchenApp_V2.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepo, IMapper mapper, IOptions<AppSettings> appsettings)
        {
            _userRepo = userRepo;
            _appSettings = appsettings.Value;
            _mapper = mapper;
        }

        //Methods
        public async Task<IEnumerable<User>> ReadAllAsync()
        {
            return await _userRepo.ReadAllAsync();
        }

        public async Task<User> ReadOneAsync(int userId)
        {
            return await _userRepo.ReadOneAsync(userId);
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var result =  await _userRepo.AuthenticateAsync(username, password);

            //user not found
            if (result == null)
            {
                return null;
            }

            if (!VerifyPassword(password, result.PasswordHash, result.PasswordSalt))
                return null;

            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, result.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Token = tokenHandler.WriteToken(token);
            return result;
        }

        public async Task<User> CreateAsync(UserRegistration user, string password)
        {           
            var result = _mapper.Map<User>(user);

            //Hash password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            result.PasswordHash = passwordHash;
            result.PasswordSalt = passwordSalt;

            //Create  username
            int index = user.Email.IndexOf('@');
            result.Username = user.Email.Substring(0, index);
            result.IsAdmin = false;
            result.IsActive = false;
            result.CreatedAt = DateTime.Now;
            await _userRepo.CreateAsync(result);
            return result;
        } 

        public async Task<User> ActivateAccountAsync(UserUpdateDto user)
        {
            var objectToUpdate = _userRepo.GetRecord(user.Id);
            objectToUpdate.IsActive = user.IsActive;
            return await _userRepo.UpdateAsync(objectToUpdate);
        }

        public async Task<User> ResetPasswordAsync(UserUpdateDto user)
        {
            //Hash password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            var objectToUpdate = _userRepo.GetRecord(user.Id);
            objectToUpdate.PasswordHash = passwordHash;
            objectToUpdate.PasswordSalt = passwordSalt;
            return await _userRepo.UpdateAsync(objectToUpdate);
        }

        public async Task<User> DeleteAsync(int userId)
        {
            return await _userRepo.DeleteAsync(userId);
        }

        public bool UserExists(int userId)
        {
            return _userRepo.UserExists(userId);
        }

        public bool IsUniqueUser(string email)
        {
            return _userRepo.IsUniqueUser(email);
        }

        public bool IsPasswordValid(string password)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("[0-9]+");
            var symbol = new Regex("(\\W)+");

            return (lowercase.IsMatch(password) && uppercase.IsMatch(password) && digit.IsMatch(password) && symbol.IsMatch(password));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }

        public bool IsActiveUser(string username)
        {
            return _userRepo.IsActiveUser(username);
        }

       
    }
}
