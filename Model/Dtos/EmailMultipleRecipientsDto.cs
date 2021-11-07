using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model.Dtos
{
    public class EmailMultipleRecipientsDto
    {
        public string Recipient { get; set; }
        public int Id { get; set; }
        public DateTime? ReminderTime { get; set; }
        public decimal Price { get; set; }
    }
}
