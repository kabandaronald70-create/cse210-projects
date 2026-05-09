using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberListProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            Console.WriteLine("Enter a list of numbers, type 0 when finished.");

            while (true)
            {
                Console.Write("Enter number: ");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 0)
                {
                    break;
                }

                numbers.Add(input);
            }

            // Ensure the list is not empty before calculations
            if (numbers.Count == 0)
            {
                Console.WriteLine("No numbers were entered.");
                return;
            }

            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");

            var positiveNumbers = numbers.Where(n => n > 0).ToList();
            if (positiveNumbers.Any())
            {
                int smallestPositive = positiveNumbers.Min();
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }
            else
            {
                Console.WriteLine("No positive numbers were entered.");
            }

            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (int num in numbers)
            {
                Console.WriteLine($"  {num}");
            }
        }
    }
}