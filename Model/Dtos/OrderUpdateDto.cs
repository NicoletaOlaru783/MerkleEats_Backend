using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }       
        public string Status { get; set; }        
        public decimal Price { get; set; }
        public string KitchenComment { get; set; }
        
    }
}
