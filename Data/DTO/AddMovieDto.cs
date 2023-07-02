using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddMovieDto
    {
        public string Name { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [Required]
        public int SelectedGenreId { get; set; }
    }
}
