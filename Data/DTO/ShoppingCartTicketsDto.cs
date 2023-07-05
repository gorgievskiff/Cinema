using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ShoppingCartTicketsDto
    {
        public List<ShoppingCartTickets> Tickets { get; set; }
        public double TotalSum { get; set; }

        public ShoppingCartTicketsDto()
        {
            Tickets = new List<ShoppingCartTickets>();     
        }
    }
}
