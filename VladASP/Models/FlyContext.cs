using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladASP.Models
{
    public class FlyContext : DbContext
    {


        public DbSet<Client> clients { get; set; }
        public DbSet<Destination> destinations { get; set; }
        public DbSet<Source> sources { get; set; }
        public DbSet<Order> orders { get; set; }

        public DbSet<Flyght> flyghts { set; get; }
        public FlyContext(DbContextOptions<FlyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
