using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VladASP.Models;

namespace VladASP.Validation
{
    public class NullErr
    {
        public string errMess="Introduce o valoare";

        public bool ifNull (Object input)
        {   
            if (input !=null)
                return false;
            return true;
        }

       
        public string autentificate(Client client)
        {
            string k = "";
            if (ifNull(client.Name))
                k = k + "1";
            if (ifNull(client.Email))
                k = k + "2";
            if (client.Mobile==0)
                k = k + "3";
            if (ifNull(client.Login))
                k = k + "4";
            if (ifNull(client.Password))
                k = k + "5";
            
            return k;
        }
    }
}
