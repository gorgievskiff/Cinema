using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class DisplayMovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
        public string ImgUrl { get; set; }
        public string Genres { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
