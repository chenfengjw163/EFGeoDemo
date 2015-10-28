using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFZGeoDemo.Model
{
    public class Db : DbContext
    {
        public Db()
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
