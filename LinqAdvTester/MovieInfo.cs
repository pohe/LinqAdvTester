using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAdvTester
{
    public class MovieInfo:IComparable<MovieInfo>
    {
        public string Title { get; set; }
        public int YearSince1900 { get; set; }
        public double TimeHours { get; set; }

        public MovieInfo(string title, int yearSince1900, double timeHours)
        {
            Title = title;
            YearSince1900 = yearSince1900;
            TimeHours = timeHours;
        }


        public int CompareTo(MovieInfo other)
        {
            return Title.CompareTo(other.Title);
        }
    }

}
