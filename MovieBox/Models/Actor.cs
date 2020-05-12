using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBox.Models
{
    public class Actor
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Actor")]
        [Required]
        public string Name { get; set; }
    }
}
