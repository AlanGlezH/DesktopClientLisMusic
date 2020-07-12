using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.accounts.resources
{
    public class Account
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string cover { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public bool contentCreator { get; set; }
        public string password { get; set; }
        public string typeRegister { get; set; }
    }
}
