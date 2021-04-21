using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladASP.JoinModels
{
    public class WithSource
    {
        public int Id { set; get; }
        public bool Favorite { set; get; }
        public DateTime DataDest { set; get; }
        public DateTime DateSource { set; get; }
        public string Cod { set; get; }
        public string Source { set; get; }

        public int Price { set; get; }
        public int DestinationId { set; get; }
      
    }
}
