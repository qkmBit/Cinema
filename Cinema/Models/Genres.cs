using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class Genres
    {
        public Genres()
        {
            MovieGenre = new HashSet<MovieGenre>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public virtual ICollection<MovieGenre> MovieGenre { get; set; }
    }
}
