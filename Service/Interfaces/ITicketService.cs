using Domain.DomainModels;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITicketService
    {
        Task<int> Add(AddTicketDto ticketDto);
        Task<int> Delete(Guid ticketId);
        Task<int> Update(EditTicketDto ticketDto);
        Task<List<Ticket>> GetAll();
        Task<Ticket> GetTicketById(Guid ticketId);
        Task<AddTicketDto> GetAddViewModel();
        Task<EditTicketDto> GetEditViewModel(Guid ticketId);
        Task<List<Ticket>> FilterTicketsByDate(string date);
        Task<List<Ticket>> GetTicketsByGenreId(int genreId);
        
    }
}
