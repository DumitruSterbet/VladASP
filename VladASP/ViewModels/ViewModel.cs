using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VladASP.Models;

namespace VladASP.ViewModels
{
    public class ViewModel
    {
    
        public List<Client> clients { get; set; }
        public List<Flyght> flights { get; set; }
        public List<Order>orders { get; set; }
      
        public List<Source> sourceCities { get; set; }
        public List<Destination> destinCities { get; set; }
            }
}
