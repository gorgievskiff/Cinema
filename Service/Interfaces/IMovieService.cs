using Cinema.Data.Migrations;
using Domain.DomainModels;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMovieService
    {
        Task<int> Add(AddMovieDto movieDto);
        Task<int> Delete(int movieId);
        Task<int> Update(EditMovieDto movie);
        Task<List<DisplayMovieDto>> GetAll();
        Task<EditMovieDto> GetMovie(int movieId);
    }
}
