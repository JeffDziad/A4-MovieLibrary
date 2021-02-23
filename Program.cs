using System;

namespace A4_MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("|  Movie Library v1 |");
            Console.WriteLine("---------------------");
            Menu menu = new Menu();
            do
            {
                menu.displayMenu();
            }while(!menu.getEndProgram());

            Console.WriteLine("Thanks for using Movie Library v1!");
        }
    }
}
