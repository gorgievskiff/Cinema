using Domain.DomainModels;
using Domain.DTO;
using Repo.Implementation;
using Repo.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class TicketService : ITicketService
    {
        private ITicketDa _ticketDa;
        private IMovieService _movieService;

        public TicketService(ITicketDa ticketDa, IMovieService movieService)
        {
            _ticketDa = ticketDa;
            _movieService = movieService;

        }
        public async Task<int> Add(AddTicketDto ticketDto)
        {
            var ticket = new Ticket();
            ticket.SeatNumber = ticketDto.SeatNumber;
            ticket.Date = ticketDto.Date;
            ticket.Time = ticketDto.Time;
            ticket.Price = ticketDto.Price;
            ticket.MovieId = ticketDto.SelectedMovieId;
            ticket.IsAvailable = ticketDto.IsAvailable;

            return await _ticketDa.Add(ticket);
        }

        public async Task<AddTicketDto> GetAddViewModel()
        {
            var dto = new AddTicketDto();
            var moviesFromDb = await _movieService.GetAll();
            dto.Movies = moviesFromDb;
            return dto;
        }

        public async Task<int> Delete(Guid ticketId)
        {
            return await _ticketDa.Delete(ticketId);
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _ticketDa.GetAll();
        }

        public async Task<Ticket> GetTicketById(Guid ticketId)
        {
            return await _ticketDa.GetTicketById(ticketId);
        }

        public async Task<int> Update(EditTicketDto ticketDto)
        {
            var ticket = new Ticket();
            ticket.Price = ticketDto.Price;
            ticket.SeatNumber = ticketDto.SeatNumber;
            ticket.MovieId = ticketDto.SelectedMovieId;
            ticket.Date = ticketDto.Date;
            ticket.Time = ticketDto.Time;
            ticket.IsAvailable = ticketDto.IsAvailable;
            ticket.Id = ticketDto.Id;
            return await _ticketDa.Update(ticket);

        }

        public async Task<EditTicketDto> GetEditViewModel(Guid ticketId)
        {
            var ticketFromDb = await _ticketDa.GetTicketById(ticketId);
            var editTicketDto = new EditTicketDto();

            editTicketDto.Id = ticketId;
            editTicketDto.Price = ticketFromDb.Price;
            editTicketDto.SeatNumber = ticketFromDb.SeatNumber;
            editTicketDto.Date = ticketFromDb.Date;
            editTicketDto.Time = ticketFromDb.Time;
            editTicketDto.SelectedMovieId = ticketFromDb.MovieId;
            editTicketDto.IsAvailable = ticketFromDb.IsAvailable;

            editTicketDto.Movies = await _movieService.GetAll();

            return editTicketDto;

        }

        public async Task<List<Ticket>> FilterTicketsByDate(string date)
        {
            List<Ticket> tickets = new List<Ticket>();
            if (date != null)
            {
                tickets = await _ticketDa.FilterTicketsByDate(date);
            }
            else
            {
                tickets = await _ticketDa.GetAll();
            }
            return tickets;
        }
        public async Task<List<Ticket>> GetTicketsByGenreId(int genreId)
        {
            return await _ticketDa.GetTicketsByGenreId(genreId);  
        }
        
    }
}
