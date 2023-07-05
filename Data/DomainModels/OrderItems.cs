using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
	public class OrderItems
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("ShoppingCartTickets")]
        public int ShoppingCartTicketsId { get; set; }
        public ShoppingCartTickets ShoppingCartTickets { get; set; }
    }
}
