using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
	public class ListPaidOrdersDto
	{
        public double TotalSum { get; set; }
        public DateTime OrderDate { get; set; }
        public List<PaidMovieTicketDto> PaidTicketMovies { get; set; }
    }
}
