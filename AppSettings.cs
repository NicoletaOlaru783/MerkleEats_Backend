using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ApiKey { get; set; }
        public string LinkConfirm { get; set; }
        public string LinkCancel { get; set; }
    }
}
