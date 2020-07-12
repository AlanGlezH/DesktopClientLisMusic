using LisMusic.accounts.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.Utils
{
    class SingletonSesion
    {
        private static LoginResponse accountSesion = null;

        private SingletonSesion() { }

        public static void SetSingletonSesion(LoginResponse account)
        {
            if(accountSesion == null)
            {
                accountSesion = account;
            }
           
        }

        public static LoginResponse GetSingletonSesion()
        {
            return accountSesion;
        }



    }
}
