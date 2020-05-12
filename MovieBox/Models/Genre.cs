using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBox.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Genre")]
        [Required]
        public string Name { get; set; }
    }
}
