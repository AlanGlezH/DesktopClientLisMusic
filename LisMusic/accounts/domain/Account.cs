using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.accounts.domain
{
    public class Account
    {
        public string idAccount { get; set; }
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

        public Account(string firstName, string lastName, string email, string userName, string gender, string birthday, string cover, string created, string updated, bool contentCreator, string password, string typeRegister)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.userName = userName;
            this.gender = gender;
            this.birthday = birthday;
            this.cover = cover;
            this.created = created;
            this.updated = updated;
            this.contentCreator = contentCreator;
            this.password = password;
            this.typeRegister = typeRegister;
        }

        public Account()
        {
        }
    }
}
