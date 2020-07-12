using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.accounts.resources
{
    
    public class LoginResponse
    {
        public Account account { get; set; }
        public string access_token { get; set; }
    }
}
