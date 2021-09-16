using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model
{
    public class OrderItem
    {
        public int Id { get; set; }    
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        //public int ProductId { get; set; }
        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }  
    }
}
