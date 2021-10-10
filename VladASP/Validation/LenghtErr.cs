using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VladASP.Models;

namespace VladASP.Validation
{
    public class LenghtErr
    {
        public string errMess = "Introdu cel putin 4 simboluri";

        public bool ifNotEnought(Object input)
        {
            if (input.ToString().Length>3)
                return false;
            return true;
        }


        public string autentificate(Client client)
        {
            string k = "";
            if (ifNotEnought(client.Name))
                k = k + "1";
            if (ifNotEnought(client.Email))
                k = k + "2";
            if (ifNotEnought(client.Mobile))
                k = k + "3";
            if (ifNotEnought(client.Login))
                k = k + "4";
            if (ifNotEnought(client.Password))
                k = k + "5";

            return k;
        }
    }
}
