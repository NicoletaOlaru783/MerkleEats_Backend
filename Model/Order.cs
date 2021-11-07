using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model
{

    public class Order

    {
        public int Id { get; set; }
        public string UID { get; set; }
        public string DeliveryType { get; set; }
        public string Status { get; set; }
        public string UserComment { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReminderTime { get; set; }
        public string KitchenComment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Email { get; set; }
        public DateTime? ConfirmedAt { get; set; }


        //Foreign key to User => Not in use at the moment
        //public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public User User { get; set; }
    }

}
