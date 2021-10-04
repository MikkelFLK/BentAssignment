﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BentAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get Primes from range (seq)");
            Console.WriteLine("2) Get Primes from range (par)");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    primeRangeSeq();
                    return true;
                case "2":
                    primeRangePara();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

        private static void primeRangeSeq()
        {
            Console.Clear();
            Console.WriteLine("Enter two numbers to get primes between the numbers: Type q to Quit");
            while (true)
            {
                string startNumber;
                string endNumber;
                Console.Write("Enter The Start Number:");
                startNumber = (Console.ReadLine());
                if (startNumber.Contains("q"))
                {
                    break;
                }
                Console.Write("Enter the End Number : ");
                endNumber = (Console.ReadLine());
                if (endNumber.Contains("q"))
                {
                    break;
                }
                long start = long.Parse(startNumber);
                long end = Convert.ToInt64(endNumber);
                IList<long> numbers = CreateRange(start, end).ToList();
                Stopwatch sw1 = Stopwatch.StartNew();
                List<long> NumOfPrims = (List<long>)GetPrimeList(numbers);
                sw1.Stop();
                NumOfPrims.Sort();
                foreach (var primes in NumOfPrims)
                {
                    Console.WriteLine(primes);
                }
                Console.WriteLine("Time = {0:F5}", (sw1.ElapsedMilliseconds / 1000f));
            }
        }

        private static void primeRangePara()
        {
            Console.Clear();
            Console.WriteLine("Enter two numbers to get primes between the numbers: Type q to Quit");
            while (true)
            {
                string startNumber;
                string endNumber;
                Console.Write("Enter The Start Number:");
                startNumber = (Console.ReadLine());
                if (startNumber.Contains("q"))
                {
                    break;
                }
                Console.Write("Enter the End Number : ");
                endNumber = (Console.ReadLine());
                if (endNumber.Contains("q"))
                {
                    break;
                }
                long start = long.Parse(startNumber);
                long end = Convert.ToInt64(endNumber);
                IList<long> numbers = CreateRange(start, end).ToList();
                Stopwatch sw1 = Stopwatch.StartNew();
                List<long> NumOfPrims = (List<long>)GetPrimeListWithParallel(numbers);
                sw1.Stop();
                NumOfPrims.Sort();
                foreach (var primes in NumOfPrims)
                {
                    Console.WriteLine(primes);
                }
                Console.WriteLine("Time = {0:F5}", (sw1.ElapsedMilliseconds / 1000f));
            }
        }

        private static IEnumerable<long> CreateRange(long start, long end)
        {
            var limit = start + end;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }

        private static IList<long> GetPrimeList(IList<long> numbers)
        {
            var primeNumbers = new ConcurrentBag<long>();

            foreach (var number in numbers)
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            };
            return primeNumbers.ToList();
        }

        private static IList<long> GetPrimeListWithParallel(IList<long> numbers)
        {
            var primeNumbers = new ConcurrentBag<long>();

            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });

            return primeNumbers.ToList();
        }

        private static bool IsPrime(long number)
        {
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}


