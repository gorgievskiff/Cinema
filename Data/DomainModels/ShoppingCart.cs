using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Domain.DomainModels
{
    public class ShoppingCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
       
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public virtual ICollection<ShoppingCartTickets> Tickets { get; set; }

    }
}
