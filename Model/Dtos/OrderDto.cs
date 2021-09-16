using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string DeliveryType { get; set; }
        public string Status { get; set; }
        public string UserComment { get; set; }
        public decimal Price { get; set; }
        public string KitchenComment { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }

        ////Foreign key to User
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
