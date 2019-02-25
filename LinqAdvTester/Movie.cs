using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAdvTester
{
    public class Movie
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int DurationInMins { get; set; }
        public string StudioName { get; set; }

        public List<Actor> Actors { get; set; }

        public Movie()
        {
        }
        public Movie(string title, int year, int durationInMins, string studioName)
        {
            Title = title;
            Year = year;
            DurationInMins = durationInMins;
            StudioName = studioName;

        }
    }
}
