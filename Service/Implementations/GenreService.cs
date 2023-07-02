using Domain.DomainModels;
using Repo.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class GenreService : IGenreService
    {
        private IGenreDa _genreDa;
        public GenreService(IGenreDa genreDa)
        {
                _genreDa = genreDa;
        }
        public Task<int> Add(Genre genre)
        {
           return _genreDa.Add(genre);
        }

        public async Task<int> Delete(int genreId)
        {
            return await _genreDa.Delete(genreId);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _genreDa.GetAll();
        }


        public async Task<Genre> GetGenre(int genreId)
        {
            return await _genreDa.GetGenreById(genreId);
        }

        public async Task<int> Update(int genreId, string genreName)
        {
            return await _genreDa.Update(genreId, genreName);   
        }
    }
}
