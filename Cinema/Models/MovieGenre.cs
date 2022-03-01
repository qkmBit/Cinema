using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class MovieGenre
    {
        public long MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genres Genre { get; set; }
        public virtual Film Movie { get; set; }
    }
}
