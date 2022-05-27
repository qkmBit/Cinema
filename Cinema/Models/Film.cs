using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class Film
    {
        public Film()
        {
            MovieGenre = new List<MovieGenre>();
            Session = new List<Session>();
        }

        public long FilmId { get; set; }
        public string FilmName { get; set; }
        public string? FilmDescription { get; set; }
        public DateTime FilmDate { get; set; }
        public int FilmDuration { get; set; }
        public bool? Afisha { get; set; }
        public string FilmDirector { get; set; }
        public string Poster { get; set; }
        public string AfishaImg { get; set; }
        public string TrailerUrl { get; set; }
        public string Country { get; set; }

        public virtual List<MovieGenre> MovieGenre { get; set; }
        public virtual List<Session> Session { get; set; }
    }
}
