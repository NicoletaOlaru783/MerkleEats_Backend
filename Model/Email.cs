using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model
{
    public class Email
    {
        public string Recipient { get; set; }
        public int Id { get; set; }
        public DateTime? ReminderTime { get; set; }
        public decimal Price { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
    }
}
