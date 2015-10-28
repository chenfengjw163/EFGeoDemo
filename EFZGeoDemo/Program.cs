using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFZGeoDemo.Model;
using static System.Int32;

namespace EFZGeoDemo
{
    class Program
    {
        private static List<TestLocaion> _locations;
        public static void Main(string[] args)
        {
            var db = new Db();
            if (!db.Hotels.Any())
            {
                InitData(db);
            }
            while (true)
            {
                Console.WriteLine("请输入半径：");
                var limit = Console.ReadLine();
                var intLimit = 0;
                TryParse(limit, out intLimit);
                Console.WriteLine("请输入坐标：xxx,xxx");
                var locs = Console.ReadLine();
                var locsSps = locs?.Split(',');
                if (locsSps?.Length != 2)
                {
                    continue;
                }
                var point = DbGeography.PointFromText($"Point({locsSps[0]} {locsSps[1]})", 4326);
                var query = db.Hotels.Where(_ => _.Geo.Distance(point) < intLimit).ToList();
                Console.WriteLine("符合条件的有：");
                foreach (var hotel in query)
                {
                    Console.WriteLine($"Name：{hotel.Name},Point:{hotel.Geo.Longitude},{hotel.Geo.Latitude}");
                }
            }
            
        }

        public static void InitData(Db db)
        {
            _locations = new List<TestLocaion>
                {
                    new TestLocaion()
                    {
                        Lon = 118.797655,
                        Lat = 32.041222,
                        Name = "中央饭店"
                    },
                    new TestLocaion()
                    {
                        Lon = 118.797269,
                        Lat = 32.044278,
                        Name = "总统府"
                    },
                    new TestLocaion()
                    {
                        Lon = 118.798642,
                        Lat = 32.048352,
                        Name = "雄狮国际大厦"
                    },
                    new TestLocaion()
                    {
                        Lon = 118.804479,
                        Lat = 32.051698,
                        Name = "金鹏饭店"
                    },
                    new TestLocaion()
                    {
                        Lon = 118.81701,
                        Lat = 32.060573,
                        Name = "白马公园"
                    }

                };
            foreach (var loc in _locations)
            {
                db.Hotels.Add(new Hotel()
                {
                    Geo = DbGeography.PointFromText($"Point({loc.Lon} {loc.Lat})", 4326),
                    Name = loc.Name
                });
            }
            db.SaveChanges();
        }
    }

    public class TestLocaion
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string Name { get; set; }
    }
}
