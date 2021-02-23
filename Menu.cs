using System;
using A4_MovieLibrary.Data;

namespace A4_MovieLibrary
{
    class Menu
    {
        private Boolean endProgram = false;
        private MovieManager movieManager = new MovieManager();
        private FileManager fileManager = new FileManager();
        public void displayMenu()
        {
            DisplayMenu();
        }

        public Boolean getEndProgram()
        {
            return this.endProgram;
        }

        private void DisplayMenu()
        {
            Console.WriteLine("---Main Menu---");
            Console.WriteLine("1. List Movies");
            Console.WriteLine("2. Add Movie to List");
            Console.WriteLine("3. Exit");
            HandleUserInput();
        }

        private void HandleUserInput()
        {
            Console.Write(">");
            try
            {
                int userInput = Convert.ToInt32(Console.ReadLine());
                HandleMenuSelection(userInput);
            }catch(Exception fe)
            {
                Logging.log("That is not a valid menu option!", fe);
                HandleUserInput();
            }
        }

        private void HandleMenuSelection(int input)
        {
            switch(input)
            {
                case 1:
                    //list movies
                    Console.Clear();
                    SearchMaterial();
                    break;
                case 2:
                    //add a movie
                    Console.Clear();
                    startMovieCreation();
                    break;
                case 3:
                    endProgram = true;
                    break;
            }
        }

        public void startMovieCreation()
        {
            movieManager.createMovie();
        }

        public void SearchMaterial()
        {
            Console.WriteLine("Choose how you would like to search:");
            Console.WriteLine("1. By Genre");
            Console.WriteLine("2. Title Keyword (Including Year)");
            Console.WriteLine("3. List all Movies");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write(">");
            try
            {
                int userInput = Convert.ToInt32(Console.ReadLine());
                HandleSearchMaterialSelection(userInput);
            }catch(Exception ex)
            {
                Logging.log("That is not a valid menu option!", ex);
            }
        }

        public void HandleSearchMaterialSelection(int input)
        {
            switch(input)
            {
                case 1:
                    Console.Clear();
                    fileManager.searchByGenrePrompt();
                    break;
                case 2:
                    Console.Clear();
                    fileManager.searchByKeyword();
                    break;
                case 3:
                    Console.Clear();
                    fileManager.listAllMovieTitles();
                    break;
                case 4:
                    Console.Clear();
                    DisplayMenu();
                    break;
                default:
                    Logging.logX("That is not a valid menu option!");
                    SearchMaterial();
                    break;
            }
        }
    }
}