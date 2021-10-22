using System;
using IDAL.DO;


public enum UserOptions
{
    Add = 1, Update, Display, ListDisplay
}


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the delivery system by skimmers:\n1 " +
                "- Insert options.\n2 - Update options.\n3 - Display options.\n4 " +
                "- List view options.\n0 - Exit.");
    
            int Option = int.Parse(Console.ReadLine()) ;


            switch (Option)
            {
                case UserOptions.Add:
                    // code block
                    break;
                case y:
                    // code block
                    break;
                default:
                    // code block
                    break;
            }

        }
    }

}
