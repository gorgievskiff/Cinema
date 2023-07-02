using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EditTicketDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string Time { get; set; }
        [Required]
        public int SelectedMovieId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int SeatNumber { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public List<DisplayMovieDto> Movies { get; set; }
    }
}
