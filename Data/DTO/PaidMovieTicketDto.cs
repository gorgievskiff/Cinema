using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
	public class PaidMovieTicketDto
	{
        public string MovieName { get; set; }
        public string ImgUrl { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
