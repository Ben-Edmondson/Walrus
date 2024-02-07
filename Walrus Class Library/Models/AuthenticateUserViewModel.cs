using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Walrus_Class_Library.Models
{
    public class AuthenticateUserViewModel
    {
        public UserPresenceViewModel? User { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
