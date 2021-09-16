using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class OrderConfirmDto
    {
        public bool IsConfirmed { get; set; }
        public string Email { get; set; }
    }
}
