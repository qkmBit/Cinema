using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Models;

namespace Cinema.ViewModels
{
    public class IndexModel
    {
        private readonly CinemaContext context;
        public Film Film { get; set; }
        public Users User { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Session Session { get; set; }
        public List<Film> Films { get; set; }
        public List<Genres> Genres { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Hall> Halls { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }

        public IndexModel( CinemaContext context)
        {
            this.context = context;
        }
        public bool buyTickets(string userPhone, List<int> ticketsId)
        {
            User = context.Users.Where(u => u.PhoneNumber == userPhone).FirstOrDefault();
            List<Ticket> tickets = new List<Ticket>();
            foreach(var ticketId in ticketsId)
            {
                if (context.Ticket.Where(t => t.TicketId == ticketId).FirstOrDefault().OrderID == null)
                {
                    tickets.Add(context.Ticket.Where(t => t.TicketId == ticketId).FirstOrDefault());
                }
                else
                {
                    return true;
                }
            }
            Tickets = tickets;
            return false;
        }
        public void getSession(long id)
        {
            Tickets = context.Ticket.Where(ticketId => ticketId.SessionId == id).ToList();
            Halls = context.Hall.ToList();
            Session = context.Session.Where(s => s.SessionId==id).FirstOrDefault();
            Films = context.Film.ToList();
        }
        public void getFilm(long id)
        {
            MovieGenres = context.MovieGenre.ToList();
            Halls = context.Hall.ToList();
            Genres = context.Genres.ToList();
            Sessions = context.Session.Where(d => d.SessionDateTime > DateTime.Now).ToList();
            Film = context.Film.Where(f => f.FilmId == id).FirstOrDefault();

        }
        public void onGet()
        {
            MovieGenres = context.MovieGenre.ToList();
            Halls = context.Hall.ToList();
            Genres = context.Genres.ToList();
            Sessions = context.Session.Where(d => d.SessionDateTime>DateTime.Now).ToList();
            Films = context.Film.Where(f => f.Session.Count > 0).ToList();
        }
    }
}
