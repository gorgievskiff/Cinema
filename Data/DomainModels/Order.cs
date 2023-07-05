using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
	public class Order
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double TotalSum { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItems> OrderedItems { get; set; }
        public string UserId { get; set; }

    }
}
