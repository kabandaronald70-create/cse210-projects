using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello World! This is the Exercise3 Project.");

        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int gues = -1;
        while (gues != magicNumber)
        {
            Console.Write("What is your guess? ");
            gues = int.Parse(Console.ReadLine());

            if (magicNumber > gues)
            {
                Console.WriteLine("Higher");
            }
            else if (gues > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }

    }
}