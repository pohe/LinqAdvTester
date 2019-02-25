using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAdvTester
{
    public class CarComparer : IEqualityComparer<Car>
    {
        public bool Equals(Car x, Car y)
        {
            return x.Plate == y.Plate;
        }

        public int GetHashCode(Car obj)
        {
            return new { obj.Plate, obj.Price, obj.Description }.GetHashCode();
        }
    }

}
