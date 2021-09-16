using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class OrderCreateDto
    {
        public string DeliveryType { get; set; }        
        public string UserComment { get; set; }
        public string Email { get; set; }
    }
}
