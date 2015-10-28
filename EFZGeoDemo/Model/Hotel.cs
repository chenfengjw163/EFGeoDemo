using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFZGeoDemo.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Geo { get; set; }
    }
}
