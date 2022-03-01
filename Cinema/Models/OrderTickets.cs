using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public partial class OrderTickets
    {
        public int OrderId { get; set; }
        public long TicketId { get; set; }

        public virtual UserOrders Order { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
