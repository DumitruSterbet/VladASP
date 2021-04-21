using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladASP.Models
{
    public class Flyght
    {
        public bool Favorite { set; get; }

        public int Id { set; get; }

        public DateTime DateSource { set; get; }
        public DateTime DataDest { set; get; }
        public string Cod { set; get; }
        public int DestinationId { set; get; }
        public Destination Destination { set; get; }
        public int Price { set; get; }
        public int SourceId { set; get; }
        public Source Source { set; get; }
    }
}
