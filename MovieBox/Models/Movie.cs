using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBox.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public double Price { get; set; }







    }
}
