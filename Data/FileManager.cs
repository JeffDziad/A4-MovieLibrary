using System;
using A4_MovieLibrary.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using ConsoleTables;



namespace A4_MovieLibrary.Data
{
    class FileManager
    {
        private static readonly string filepath = Path.Combine(Environment.CurrentDirectory, "File", "movies.csv"); 
        private List<Movie> movies;
        private static string[] possibleGenres = {"Action", "Adventure", "Animation", "Children", "Comedy", "Crime", "Documentary", "Drama", "Fantasy", "Film-Noir", "Horror", "Musical", "Mystery", "Romance", "Sci-Fi", "Thriller", "War", "Western"}; 
        private static List<String> chosenGenres;
        public FileManager()
        {
            if(!File.Exists(filepath))
            {
                Logging.logX($"The file {filepath} could not be found!");
            }
        }

        public void addMovie(List<string> movieDetails)
        {
            try
            {
                using(StreamWriter sw = new StreamWriter(filepath, true))
                {
                    string output = String.Join(",", movieDetails.ToArray());
                    sw.WriteLine(output);
                    sw.Close();
                }
            }catch(FileNotFoundException fnfe)
            {
                Logging.log($"The file {filepath} could not be found!", fnfe);
            }
        }

        public List<Movie> getMovieList()
        {
            generateMovieList();
            return movies;
        }

       public void generateMovieList()
       {
            movies = new List<Movie>();
            TextFieldParser parser = new TextFieldParser(filepath);
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            string[] fields;
            while(!parser.EndOfData)
            {
                fields = parser.ReadFields();
                Movie movie = new Movie();
                movie.MovieId = fields[0];
                movie.Title = fields[1];
                movie.Genres = fields[2].Replace("|", ",");
                movies.Add(movie);
            }
       }

       public void searchByGenrePrompt()
       {
            generateMovieList();
            int index = 1;
            Console.WriteLine("Possible Genres: ");
            foreach(String genre in possibleGenres)
            {
                Console.WriteLine($"{index}. {genre}");
                index++;
            }
            Console.WriteLine("Type in the genre you would like to search for. Hit enter to add another genre. ");
            searchByGenre();
       }

       public void searchByGenre()
       {
            var moviesByGenreTable = new ConsoleTable("Movie ID", "Movie Title", "Movie Genres");
            chosenGenres = new List<string>();
            string header = "Searching for: ";
            getGenreInput();
            foreach(string genre in chosenGenres)
            {
                header += "|" + genre + "|";
            }
            Console.WriteLine(header);
            foreach(Movie movie in movies)
            {
                string genre = movie.Genres;
                Boolean containsAll = chosenGenres.All(genre.Contains);
                if(containsAll)
                {
                    moviesByGenreTable.AddRow(movie.MovieId, movie.Title, movie.Genres);
                }
            }
            moviesByGenreTable.Write();
       }

       public void getGenreInput()
       {
            string userInput;
            Boolean finishedSelection = false;
            while(!finishedSelection)
            {
                Boolean validGenre = false;
                Console.Write(">");
                userInput = Console.ReadLine();
                string userInputUpper = userInput.ToUpper();
                foreach(string genre in possibleGenres)
                {
                    if(userInputUpper == genre.ToUpper())
                    {
                        chosenGenres.Add(userInput);
                        validGenre = true;
                    }
                }
                if(!validGenre)
                {
                    Logging.logX($"{userInput} is not a valid Genre. Please Try Again...");
                    getGenreInput();
                }
                Console.Write("Add another Genre (Y/N): ");
                userInput = Console.ReadLine();
                userInputUpper = userInput.ToUpper();
                char[] userInputUpperCharArr = userInputUpper.ToCharArray();
                if(userInputUpperCharArr[0] == 'N')
                {
                    finishedSelection = true;
                }
            }
       }

       public void searchByKeyword()
       {
            generateMovieList();
            List<Movie> matchingMovies = new List<Movie>();
            var moviesByKeywordTable = new ConsoleTable("Movie ID", "Movie Title", "Movie Genres");
            string keyword;
            Console.Write("Enter keyword for search: ");
            keyword = Console.ReadLine();
            string upperKeyword = keyword.ToUpper();
            foreach(Movie movie in movies)
            {
                string upperTitle = movie.Title.ToUpper();
                if(upperTitle.Contains(upperKeyword))
                {
                    matchingMovies.Add(movie);
                }
            }
            if(matchingMovies.Count == 0)
            {
                Logging.logX($"No movies matched the keyword: {keyword}");
            }else
            {
                foreach(Movie movie in matchingMovies)
                {
                    moviesByKeywordTable.AddRow(movie.MovieId, movie.Title, movie.Genres);
                }
                moviesByKeywordTable.Write();
            } 
       }

       public void listAllMovieTitles()
       {
            generateMovieList();
            foreach(Movie movie in movies)
            {
                Console.WriteLine($"{movie.MovieId}. {movie.Title}");
            }
       }
    }
}