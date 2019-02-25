using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LinqAdvTester
{
    class InsertCodeHere
    {
        List<Movie> movies = new List<Movie>()
        {
            new Movie{Title ="Se7en", Year = 1995, DurationInMins = 127, StudioName="New Line Cinema"},
            new Movie{Title = "Alien", Year = 1979, DurationInMins = 117, StudioName="20th Century Fox"},
            new Movie{Title = "Forrest Gump", Year = 1994, DurationInMins = 142, StudioName="Paramount Pictures"},
            new Movie{Title = "True Grit", Year = 2010, DurationInMins = 110, StudioName="Paramount Pictures"},
            new Movie{Title = "Dark City", Year = 1998, DurationInMins = 111, StudioName="New Line Cinema"},

        };

        List<Studio> studios = new List<Studio>()
        {
            new Studio{StudioName = "New Line Cinema", HQCity = "Boston", NoOfEmployees = 4000},
            new Studio{StudioName = "20th Century Fox", HQCity = "New York", NoOfEmployees = 2500},
            new Studio{StudioName = "Paramount Pictures", HQCity = "New York", NoOfEmployees = 8000}

        };

        public void MyCode()
        {
            List<int> numbers = new List<int> { 12, 37, 8, 17 };
            IEnumerable<int> resultA = numbers.Where(i => i < 15);


            // Corresponding SQL-like LINQ query
            //IEnumerable<int> resultB = from i in numbers
            //    where i < 15
            //    select i;

            foreach (var element in resultA)
            {
                Console.WriteLine(element);
            }

            //List<Movie> movies = new List<Movie>();
            var resultA2 = movies.Select(m => new { m.Title, m.Year });

            // Corresponding SQL-like LINQ query
            //var resultB2 = from m in movies
            //    select new { m.Title, m.Year };

            foreach (var element in resultA2)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("ResultA3");
            var resultA3 = movies.Select(m => new { m.Title, m.Year })
                .Where(m => m.Year > 1995);

            foreach (var element in resultA3)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("ResultA4");
            // More peculiar than useful...
            var resultA4 = movies.Select(m => new { m.Title, m.Year })
                .Select(m => new { ShortYear = m.Year, m.Title })
                .Where(m => m.ShortYear > 1995)
                .Where(m => m.Title.Contains("True"));

            foreach (var element in resultA4)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("Linq for data transformation at the object level");

            List<MovieInfo> miList = movies
                .Select(m => new MovieInfo(
                    m.Title,
                    m.Year - 1900,
                    m.DurationInMins / 60.0))
                .ToList();
            
            foreach (MovieInfo movieInfo in miList)
            {
                Console.WriteLine($"MovieInfo {movieInfo.Title}\t {movieInfo.YearSince1900}\t {movieInfo.TimeHours:0.00}");
            }

            //IEnumerable does not contain eg a Sort method
            Console.WriteLine("After sorting List that contains MovieInfo objects");
            miList.Sort();

            foreach (MovieInfo movieInfo in miList)
            {
                Console.WriteLine($"MovieInfo {movieInfo.Title}\t {movieInfo.YearSince1900}\t {movieInfo.TimeHours:0.00}");
            }


            //Conversion to a Dictionary
            Console.WriteLine("Conversion to a Dictionary");
            Dictionary<string, Movie> movieDict = movies.ToDictionary(m => m.Title);
            foreach (var movieValuepair in movieDict)
            {
                Console.WriteLine($"Title {movieValuepair.Key}");
            }

            // Actions on Object level

            Console.WriteLine("Actions on Object level- printing using .Foreach");
            miList.ForEach(m => Console.WriteLine(m.Title));

            Console.WriteLine("We want to print a list of movies without an ToString method");
            movies
                .Select(m => $"{m.Title}, made in {m.Year}")
                .ToList()
                .ForEach(Console.WriteLine);

            Console.WriteLine("The Aggregate method");
            List<int> numbers2 = new List<int> { 2, 8, 4, 3 };

            int sosB = numbers2.Aggregate(
                (val, item) => val + (item * item)); // Accumulator function
            Console.WriteLine($"Sum-of-squares is {sosB}");

            int sosC = numbers2.Aggregate(
                0, // Seed value
                (val, item) => val + (item * item)); // Accumulator function
            Console.WriteLine($"Sum-of-squares is {sosC}");

            double sosAverage = numbers2.Aggregate(
                0, // Seed value
                (val, item) => val + (item * item), // Accumulator function
                val => (val * 1.0) / numbers2.Count); // Result selector function
            Console.WriteLine($"Sum-of-squares average is {sosAverage}");

            List<int> sod= numbers2.Select(n => n * n).ToList();
            sod.ForEach(Console.WriteLine);
            Console.WriteLine("Sum of squares " + sod.Sum());

            string numberList= numbers.Aggregate("", (val, item) => val + " " + item,
                l => l + " ialt antal tal i listen er " + numbers2.Count);
            Console.WriteLine(numberList);


            Console.WriteLine("Set operations with LINQ");
            List<int> setA = new List<int> { 2, 6, 12, 9, 3, 7 };
            List<int> setB = new List<int> { 12, 8, 3, 71, 13 };

            Console.WriteLine("In A but not in B: ");
            foreach (int val in setA.Except(setB))
            {
                Console.WriteLine(val);
            }


            Car carA = new Car("AA 111", 10000, "Car A");
            Car carB = new Car("BB 222", 20000, "Car B");
            Car carC = new Car("AA 111", 10000, "Car A");
            Car carD = new Car("DD 444", 40000, "Car D");
            Car carE = new Car("EE 555", 50000, "Car E");

            List<Car> carSetA = new List<Car> { carA, carB, carD, carE };
            List<Car> carSetB = new List<Car> { carB, carC, carE };

            Console.WriteLine("Without CarComparer");
            foreach (Car val in carSetA.Except(carSetB))
            {
                Console.WriteLine(val.Plate + " " + val.Description);
            }

            Console.WriteLine("Using CarComparer");
            foreach (Car val in carSetA.Except(carSetB, new CarComparer()))
            {
                Console.WriteLine(val.Plate + " " + val.Description);
            }

            Console.WriteLine("Standard LinQ");

            Stopwatch watch = new Stopwatch();
            watch.Restart();
            IEnumerable<int> primes = Enumerable.Range(2, 10000000).Where(IsPrime);
            int primesCount = primes.Count();
            watch.Stop();
            Console.WriteLine($"Primes up to 10,000,000: {primesCount}");
            Console.WriteLine($"Time spent: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("PLinQ");

            watch.Restart();
            IEnumerable<int> primesParallelQuery = Enumerable.Range(2, 10000000)
                .AsParallel()
                .Where(IsPrime);
            int primesParallelCount = primesParallelQuery.Count();
            watch.Stop();
            Console.WriteLine($"Primes up to 10,000,000: {primesCount}");
            Console.WriteLine($"Time spent: {watch.ElapsedMilliseconds} ms");


            Console.WriteLine("Foreach");
            List<int> primeList = new List<int>();
            Enumerable.Range(2, 1000000)
                .AsParallel()
                .Where(IsPrime)
                .ForAll(primeList.Add);
            Console.WriteLine($"No. of items in primesList : {primeList.Count}");


            ConcurrentBag<int> primeListThreadSafe = new ConcurrentBag<int>();
            Enumerable.Range(2, 1000000)
                .AsParallel()
                .Where(IsPrime)
                .ForAll(primeListThreadSafe.Add);
            Console.WriteLine($"No. of items in primesList : {primeListThreadSafe.Count}");


        }

        private bool IsPrime(int number)
        {
            int limit = Convert.ToInt32(Math.Sqrt(number));
            bool isPrime = true;

            for (int i = 2; i <= limit && isPrime; i++)
            {
                isPrime = number % i != 0;
            }

            return isPrime;
        }

    }
}
