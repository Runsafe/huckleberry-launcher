using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuckleberryLauncher
{
    class AuthenticateRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string clientToken { get; set; }
        public AuthenticationAgent agent { get; set; }
    }
}
