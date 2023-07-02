using Domain.DomainModels;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Implementation
{
    public class MovieDa : IMovieDa
    {
        private static ILogger<MovieDa> _logger;
        private readonly ApplicationDbContext _db;
        public MovieDa(ILogger<MovieDa> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<int> Add(AddMovieDto movieDto)
        {
            try
            {
                var movie = new Movie();
                movie.Name = movieDto.Name;
                movie.Year = movieDto.Year;
                movie.ImgUrl = movieDto.ImgUrl;
                movie.Rating = movieDto.Rating;

                _db.Movies.Add(movie);
                await _db.SaveChangesAsync();
                int movieId = movie.Id;

                var movieGenre = new MovieGenre();
                movieGenre.MovieId = movieId;
                movieGenre.GenreId = movieDto.SelectedGenreId;
                _db.MovieGenres.Add(movieGenre);

                return await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<int> Delete(int movieId)
        {
            try
            {
                var movieFromDb = await _db.Movies.Where(x => x.Id == movieId).FirstOrDefaultAsync();
                var movieGenres = await _db.MovieGenres.Where(x => x.MovieId == movieId).ToListAsync();

                _db.MovieGenres.RemoveRange(movieGenres);
                _db.Movies.Remove(movieFromDb);

                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<List<Movie>> GetAll()
        {
            try
            {
                return await _db.Movies.Include(x => x.MovieGenres).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Movie> GetMovieById(int movieId)
        {
            try
            {
                return await _db.Movies
                    .Include(x => x.MovieGenres)
                    .Where(x => x.Id == movieId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<List<MovieGenre>> GetMovieGenresByMovieId(int movieId)
        {
            try
            {
                return await _db.MovieGenres.Include(x => x.Genre).Where(x => x.MovieId == movieId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<int> Update(EditMovieDto movieDto)
        {
            try
            {
                var movieFromDb = await _db.Movies.Where(x => x.Id == movieDto.Id).FirstOrDefaultAsync();
                if(movieFromDb != null)
                {
                    movieFromDb.Name = movieDto.Name;
                    movieFromDb.Year = movieDto.Year;
                    movieFromDb.ImgUrl = movieDto.ImgUrl;
                    movieFromDb.Rating = movieDto.Rating;

                    _db.Update(movieFromDb);

                    var genresFromDb = await _db.MovieGenres.Where(x => x.MovieId == movieDto.Id).FirstOrDefaultAsync();
                    if(genresFromDb != null)
                    {
                        genresFromDb.GenreId = movieDto.SelectedGenreId;
                        _db.Update(genresFromDb);
                    }

                    return await _db.SaveChangesAsync();
                }
                return 0;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }

}
