using System;
using IDAL.DO;





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
                case (int)UserOptions.Exit:

                    break;
                case (int)UserOptions.Add:
                    Console.WriteLine("please select:\n" +
                        "1-Add base-station\n" +
                        "2-Add Drone\n" +
                        "3-Add a new costumer\n" +
                        "4-Add a new Parcel to a costumer"
                       );
                    // code block
                    break;

                case (int)UserOptions.Update:
                    break;
                case (int)UserOptions.Display:
                    // code block
                    break;
                case (int)UserOptions.ListDisplay:
                    // code block
                    break;
           
                default:
                    break;


            }

        }
    }

}
