using System;

class Program
{
    static void Main(string[] args)
    {
        // Core Requirement 1: Ask user for grade percentage
        Console.Write("Enter your grade percentage (0-100): ");
        int percentage = int.Parse(Console.ReadLine());

        // Core Requirement 3: Create a variable to store the letter grade
        string letter = "";

        // Core Requirement 2: Determine letter grade using if/else if
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        string sign = "";
        int lastDigit = percentage % 10;

        if (letter != "F")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }

        }

        if (letter == "A")
        {
            if (sign == "+")
            {
                sign = "";
            }
        }

        if (letter == "F")
        {
            sign = "";
        }

        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! Better luck next time.");
        }
    }
}