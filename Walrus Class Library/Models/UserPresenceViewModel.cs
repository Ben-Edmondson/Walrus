using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus_Class_Library.Models
{
    public class UserPresenceViewModel
    {
        public string UID { get; set; }
        public string ConnectionStatus { get; set; }
        public DateTime? LastOnline { get; set; }

    }
}
