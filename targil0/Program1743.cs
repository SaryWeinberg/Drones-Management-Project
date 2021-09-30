using System;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome1743();
            Welcome2385();
            Console.ReadKey();
        }
        static partial void Welcome2385();

        private static void Welcome1743()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
