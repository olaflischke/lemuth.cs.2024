using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzbWaehrungenDal
{
    public class WaehrungsContext : DbContext
    {
        public WaehrungsContext(DbContextOptions<WaehrungsContext> options) : base(options) { }

        public DbSet<Handelstag> Handelstage { get; set; }
        public DbSet<Waehrung> Waehrungen { get; set; }
    }
}
