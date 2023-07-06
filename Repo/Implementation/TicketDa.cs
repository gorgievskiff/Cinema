using Domain.DomainModels;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Implementation
{
    public class TicketDa : ITicketDa
    {
        private static ILogger<TicketDa> _logger;
        private readonly ApplicationDbContext _db;
        public TicketDa(ILogger<TicketDa> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<int> Add(Ticket ticketDto)
        {
            try
            {
                _db.Tickets.Add(ticketDto);
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<int> Delete(Guid ticketId)
        {
            try
            {
                var ticketFromDb = await _db.Tickets.Where(x => x.Id == ticketId).FirstOrDefaultAsync();
                _db.Tickets.Remove(ticketFromDb);
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<List<Ticket>> GetAll()
        {
            try
            {
                var ticketsFromDb = await _db.Tickets
                                           .Include(x => x.Movie)
                                           .Include(x => x.Movie.MovieGenres)
                                           .AsNoTracking()
                                           .ToListAsync();
                return ticketsFromDb;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }


        public async Task<Ticket> GetTicketById(Guid ticketId)
        {
            try
            {
               return await _db.Tickets
                    .Include(x => x.Movie)
                    .Where(x => x.Id == ticketId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> Update(Ticket ticketDto)
        {
            try
            {
                var ticketFromDb = await _db.Tickets
                    .Where(x => x.Id == ticketDto.Id)
                    .FirstOrDefaultAsync();
                ticketFromDb.Price = ticketDto.Price;
                ticketFromDb.SeatNumber = ticketDto.SeatNumber;
                ticketFromDb.MovieId = ticketDto.MovieId;
                ticketFromDb.Date = ticketDto.Date;
                ticketFromDb.Time = ticketDto.Time;
                ticketFromDb.IsAvailable = ticketDto.IsAvailable;
                _db.Update(ticketFromDb);
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<List<Ticket>> FilterTicketsByDate(string date)
        {
            try
            {
                var castedDate = Convert.ToDateTime(date);
                return await _db.Tickets
                                .Include(x => x.Movie)
                                .Include(x => x.Movie.MovieGenres)
                                .Where(x => x.Date.Date == castedDate.Date).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
         
        public async Task<List<Ticket>> GetTicketsByGenreId(int genreId)
        {
            try
            {
                var movieGenres = await _db.MovieGenres.Include(x => x.Movie).ToListAsync();
                movieGenres = movieGenres.Where(x => x.GenreId == genreId).ToList();

                var allTickets = await _db.Tickets.ToListAsync();
                var tickets = new List<Ticket>();
                
                foreach(var ticket in allTickets)
                {
                    foreach(var genre in movieGenres)
                    {
                        if(ticket.MovieId == genre.MovieId)
                        {
                            tickets.Add(ticket);
                        }
                    }
                }

                return tickets;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        
    }
}
