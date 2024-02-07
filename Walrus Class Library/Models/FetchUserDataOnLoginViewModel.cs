using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus_Class_Library.Models
{
    public class FetchUserDataOnLoginViewModel
    {
        public string LocalId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string IdToken { get; set; }
    }
}
