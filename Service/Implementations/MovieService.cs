using Domain.DomainModels;
using Domain.DTO;
using Repo.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class MovieService : IMovieService
    {
        private IMovieDa _movieDa;
        public MovieService(IMovieDa movieDa)
        {
            _movieDa = movieDa;
        }
        public async Task<int> Add(AddMovieDto movieDto)
        {
            return await  _movieDa.Add(movieDto);
        }

        public async Task<int> Delete(int movieId)
        {
            return await _movieDa.Delete(movieId);
        }

        public async Task<List<DisplayMovieDto>> GetAll()
        {
            var moviesFromDb = await _movieDa.GetAll();
            var displayMovieDtoList = new List<DisplayMovieDto>();
            foreach(var movie in moviesFromDb)
            {
                var newMovie = new DisplayMovieDto
                {
                    Id = movie.Id,
                    ImgUrl = movie.ImgUrl,
                    Name = movie.Name,
                    Rating = movie.Rating,
                    Year = movie.Year,
                };

                var genresFromDb = await _movieDa.GetMovieGenresByMovieId(movie.Id);

                foreach(var genre in genresFromDb)
                {
                    if (genre == movie.MovieGenres.Last())
                    {
                        newMovie.Genres += genre.Genre.Name;
                    }
                    else
                    {
                        newMovie.Genres = genre.Genre.Name + ",";

                    }
                }
                displayMovieDtoList.Add(newMovie);

            }
            return displayMovieDtoList;
        }

        public async Task<EditMovieDto> GetMovie(int movieId)
        {
            var movieFromDb = await _movieDa.GetMovieById(movieId);
            var editMovieDto = new EditMovieDto();

            editMovieDto.Name = movieFromDb.Name;
            editMovieDto.Year = movieFromDb.Year;
            editMovieDto.SelectedGenreId = movieFromDb.MovieGenres.Select(x => x.GenreId).FirstOrDefault();
            editMovieDto.ImgUrl = movieFromDb.ImgUrl;
            editMovieDto.Rating = movieFromDb.Rating;
            editMovieDto.Id = movieFromDb.Id;

            return editMovieDto;
        }

        public async Task<int> Update(EditMovieDto editMovieDto)
        {
            return await _movieDa.Update(editMovieDto);
        }
    }
}
