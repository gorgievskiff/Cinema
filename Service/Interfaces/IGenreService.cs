using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGenreService
    {
        Task<int> Add(Genre genre);
        Task<int> Delete(int genreId);
        Task<int> Update(int genreId,string genreName);
        Task<List<Genre>> GetAll();
        Task<Genre> GetGenre(int genreId);
    }
}
