using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int qty { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
