using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class Film
    {
        public Film()
        {
            MovieGenre = new HashSet<MovieGenre>();
            Session = new HashSet<Session>();
        }

        public long FilmId { get; set; }
        public string FilmName { get; set; }
        public string FilmDescription { get; set; }
        public DateTime FilmDate { get; set; }
        public int FilmDuration { get; set; }
        public string FilmDirector { get; set; }

        public virtual ICollection<MovieGenre> MovieGenre { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
