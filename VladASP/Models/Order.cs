using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladASP.Models
{
    public class Order
    {
        public int Id { set; get; }
        public int ClientId { set; get; }
        public Client Client { set; get; }
        public int FlyghtId { set; get; }
        public Flyght Flyght { set; get; }
    }
}