using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsFinance { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
}
