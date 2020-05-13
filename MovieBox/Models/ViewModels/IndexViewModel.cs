using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBox.Models.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<Movie> Movie { get; set; }
        public IEnumerable<Genre> Genre { get; set; }

    }
}
