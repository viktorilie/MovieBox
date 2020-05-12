using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBox.Models.ViewModels
{
    public class MovieViewModel
    {

        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public IEnumerable<Actor> Actor { get; set; }
        public IEnumerable<Director> Director { get; set; }

    }
}
