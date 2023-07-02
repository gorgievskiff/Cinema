using Domain.DomainModels;
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
    public class GenreDa : IGenreDa
    {
        private static ILogger<GenreDa> _logger;
        private readonly ApplicationDbContext _db;
        public GenreDa(ILogger<GenreDa> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }
        public async Task<int> Add(Genre genre)
        {
            try
            {
                _db.Genres.Add(genre);
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> Delete(int genreId)
        {
            try
            {
                var genreFromDb = await _db.Genres.FindAsync(genreId);
                if(genreFromDb != null) 
                {
                    _db.Genres.Remove(genreFromDb);
                }
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }


        public async Task<List<Genre>> GetAll()
        {
            try
            {
                return await _db.Genres.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Genre> GetGenreById(int genreId)
        {
            try
            {
                return await _db.Genres.Where(x => x.Id == genreId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> Update(int genreId, string genreName)
        {
            try
            {
                var genreFromDb = await _db.Genres.Where(x => x.Id == genreId).FirstOrDefaultAsync();
                if(genreFromDb != null)
                {
                    genreFromDb.Name = genreName;
                }

                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
