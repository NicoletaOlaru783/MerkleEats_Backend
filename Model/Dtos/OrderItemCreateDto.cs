using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class OrderItemCreateDto
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


    }
}
