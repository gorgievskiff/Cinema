using Domain.DomainModels;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface ITicketDa
    {
        Task<int> Add(Ticket ticketDto);
        Task<int> Delete(Guid ticketId);
        Task<int> Update(Ticket ticketDto);
        Task<List<Ticket>> GetAll();
        Task<Ticket> GetTicketById(Guid ticketId);
        Task<List<Ticket>> FilterTicketsByDate(string date);
        Task<List<Ticket>> GetTicketsByGenreId(int genreId);
        
    }
}
