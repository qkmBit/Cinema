using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderTickets = new HashSet<OrderTickets>();
        }

        public long TicketId { get; set; }
        public long SessionId { get; set; }
        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }
        public int Cost { get; set; }

        public virtual Session Session { get; set; }
        public virtual ICollection<OrderTickets> OrderTickets { get; set; }
    }
}
