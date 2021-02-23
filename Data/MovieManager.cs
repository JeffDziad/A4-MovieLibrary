using System;
using System.Collections.Generic;
using A4_MovieLibrary.Models;
using A4_MovieLibrary;

namespace A4_MovieLibrary.Data
{
    class MovieManager
    {
        private FileManager fileManager = new FileManager();
        private static string[] possibleGenres = {"Action", "Adventure", "Animation", "Children", "Comedy", "Crime", "Documentary", "Drama", "Fantasy", "Film-Noir", "Horror", "Musical", "Mystery", "Romance", "Sci-Fi", "Thriller", "War", "Western"}; 
        private List<Movie> importedMovies;
        public void createMovie()
        {
            importedMovies = fileManager.getMovieList();
            Boolean isValidMovie = true;
            string movieTitle = getTitle();
            string movieYear = getYear();
            List<string> genres = new List<string>(getGenres());
            string movieTitleYear;
            string movieGenres = String.Join("|", genres.ToArray());
            movieTitleYear = String.Format($"{movieTitle} {movieYear}");
            foreach(Movie movie in importedMovies)
            {
                string importMovieTitleYear = String.Format($"{movie.Title.ToUpper()}");
                if(movieTitleYear.ToUpper() == importMovieTitleYear)
                {
                    Logging.logX($"{movieTitleYear} alreay exists.");
                    isValidMovie = false;
                    break;
                }
            }
            if(!isValidMovie)
            {
                Menu menu = new Menu();
            }else
            {
                if(movieTitleYear.Contains(','))
                {
                    movieTitleYear = String.Format($"\"{movieTitleYear}\"");
                }
                int lastMovieIDInt = Convert.ToInt32(getLastIdNum());
                lastMovieIDInt++;
                string finalMovieID = Convert.ToString(lastMovieIDInt);

                List<string> final = new List<string>();
                final.Add(finalMovieID);
                final.Add(movieTitleYear);
                final.Add(movieGenres);
                fileManager.addMovie(final);
                Logging.logX($"Successfully added {movieTitleYear} to movies.csv");
            }
        }

        public string getLastIdNum()
        {
            Movie lastMovie = importedMovies[importedMovies.Count - 1];
            return lastMovie.MovieId;
        }

        public string getTitle()
        {
            string input;
            Console.Write("Enter Movie Title (do not include year): ");
            input = Console.ReadLine();
            return input;
        }

        public string getYear()
        {
            string output;
            Console.Write("Enter Movie Year: ");
            output = String.Format($"({Console.ReadLine()})");
            return output;
        }

        public List<string> getGenres()
        {
            List<string> output = new List<string>();
            Boolean isFinished = false;
            Boolean isValid = false;
            string input;
            string inputUpper;
            foreach(string genre in possibleGenres)
            {
                Console.WriteLine(genre);
            }
            while(!isFinished)
            {
                Console.Write("Add a genre to your movie: ");
                input = Console.ReadLine();
                inputUpper = input.ToUpper();
                foreach(string genre in possibleGenres)
                {
                    if(inputUpper == genre.ToUpper())
                    {
                        output.Add(String.Format(char.ToUpper(input[0]) + input.Substring(1)));
                        isValid = true;
                    }
                }
                if(!isValid)
                {
                    Logging.logX($"{input} is not a valid genre!");
                }else
                {
                    Console.Write("Add another genre? (Y/N): ");
                    string continueInput = Console.ReadLine();
                    continueInput = continueInput.ToUpper();
                    char[] check = continueInput.ToCharArray();
                    if(check[0] == 'N')
                    {
                        isFinished = true;
                    } 
                }
            }
            return output;
        }
    }
}