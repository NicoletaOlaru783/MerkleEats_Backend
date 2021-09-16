using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class UserAuthentication
    {
        public string Username { get; set; }      
        public string Password { get; set; }
    }
}
