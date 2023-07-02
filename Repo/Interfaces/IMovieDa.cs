using Domain.DomainModels;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface IMovieDa
    {
        Task<int> Add(AddMovieDto movieDto);
        Task<int> Delete(int movieId);
        Task<int> Update(EditMovieDto movieDto);
        Task<List<Movie>> GetAll();
        Task<Movie> GetMovieById(int movieId);
        Task<List<MovieGenre>> GetMovieGenresByMovieId(int movieId);
    }
}
